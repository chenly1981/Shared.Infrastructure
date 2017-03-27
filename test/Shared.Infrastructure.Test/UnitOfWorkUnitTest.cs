using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Autofac;
using System.Reflection;
using Shared.Infrastructure.UnitOfWork.Cassandra;

namespace Shared.Infrastructure.Test
{
    [TestClass]
    public class UnitOfWorkUnitTest : UnitTestBase
    {
        [TestMethod]
        public void ResolveUnitOfWorkProviderShouldReturnInstance()
        {
            IServiceProvider serviceProvider = InitDependencyInjection(containerBuilderAction: builder =>
            {
                builder.AddUnitOfWork();
            });

            IUnitOfWorkProvider unitOfWorkProvider = serviceProvider.GetService<IUnitOfWorkProvider>();

            Assert.IsNotNull(unitOfWorkProvider);
        }

        [TestMethod]
        public void AddEntityFramework()
        {
            IServiceProvider serviceProvider = InitDependencyInjection(services =>
            {
                services.AddDbContext<Context.TestContext>(builder =>
                {
                    builder.UseInMemoryDatabase();
                }, ServiceLifetime.Transient);
            }, containerBuilder =>
            {
                containerBuilder.AddUnitOfWork(provider =>
                {
                    provider.Register(new EntityFrameworkUnitOfWorkRegisteration("ef"));
                });
            });

            IUnitOfWorkProvider unitOfWorkProvider = serviceProvider.GetService<IUnitOfWorkProvider>();
            using (IUnitOfWork uw = unitOfWorkProvider.CreateUnitOfWork("ef"))
            {
                Assert.IsNotNull(uw);

                var entity = new Context.TestContext.TestEntity
                {
                    ID = 1,
                    Name = "name"
                };

                uw.Insert(entity);

                entity = uw.Get<Context.TestContext.TestEntity>(t => t.ID == 1);

                Assert.IsNotNull(entity);
            }
        }

        [TestMethod]
        public void AddCassandra()
        {
            IServiceProvider serviceProvider = InitDependencyInjection(services =>
            {
                services.AddLogging();
            }, containerBuilder =>
            {
                containerBuilder.AddUnitOfWork(provider =>
                {
                    provider.Register(new CassandraUnitOfWorkRegisteration(new CassandraOptions
                    {
                        Nodes = new string[] { "192.168.40.229" },
                        User = "cassandra",
                        Password = "cassandra",
                        KeySpace = "ias_dev"
                    }));                    
                });
            });

            IUnitOfWorkProvider unitOfWorkProvider = serviceProvider.GetService<IUnitOfWorkProvider>();
            using (IUnitOfWork uw = unitOfWorkProvider.CreateUnitOfWork("cassandra"))
            {
                Assert.IsNotNull(uw);

                var entityList = uw.Query<Entity.RTLSAreaDataDay>(t => t.Day == 20161129);

                Assert.IsTrue(entityList.Count > 0);
            }
        }

        [TestMethod]
        public void CannotRetriveRepositoryAcrossUnitOfWork()
        {
            IServiceProvider serviceProvider = InitDependencyInjection(services =>
            {
                services.AddLogging();
                services.AddDbContext<Context.TestContext>(builder =>
                {
                    builder.UseInMemoryDatabase();
                }, ServiceLifetime.Transient);
            }, containerBuilder =>
            {
                containerBuilder.AddUnitOfWork(provider =>
                {
                    provider.Register(new EntityFrameworkUnitOfWorkRegisteration("ef1", builder=> 
                    {

                    }));
                    provider.Register(new EntityFrameworkUnitOfWorkRegisteration("ef2"));
                });
            });

            IUnitOfWorkProvider unitOfWorkProvider = serviceProvider.GetService<IUnitOfWorkProvider>();
            using (IUnitOfWork uw = unitOfWorkProvider.CreateUnitOfWork("ef1"))
            {
                var repository = uw.CreateRepository<Repository.EntityFramework.TestEntityRepository>();
                Assert.IsNotNull(repository);

                var entityList = repository.All();
            }

            using (IUnitOfWork uw = unitOfWorkProvider.CreateUnitOfWork("ef2"))
            {
                try
                {
                    var repository = uw.CreateRepository<Repository.EntityFramework.TestEntityRepository>();
                    Assert.IsNull(repository);
                }
                catch
                {

                }
            }
        }

        [TestMethod]
        public void RepositoryMethodOverrideShouldWork()
        {
            IServiceProvider serviceProvider = InitDependencyInjection(services =>
            {
                services.AddLogging();
                services.AddDbContext<Context.TestContext>(builder =>
                {
                    builder.UseInMemoryDatabase();
                }, ServiceLifetime.Transient);
            }, containerBuilder =>
            {
                containerBuilder.AddUnitOfWork(provider =>
                {
                    provider.Register(new EntityFrameworkUnitOfWorkRegisteration("ef", builder =>
                    {
                        //builder.RegisterType<Repository.EntityFramework.TestEntityRepository>().As<Repository.Interface.ITestEntityRepository>();
                    }));
                });
            });

            IUnitOfWorkProvider unitOfWorkProvider = serviceProvider.GetService<IUnitOfWorkProvider>();
            using (var uw = unitOfWorkProvider.CreateUnitOfWork("ef"))
            {
                var entity = new Context.TestContext.TestEntity
                {
                    ID = 1,
                    Name = "hello"
                };

                uw.Insert(entity);

                entity = uw.Get<Context.TestContext.TestEntity>(t => t.ID == 1);
                
                Assert.AreEqual(entity.Name, "override");
            }
        }
    }

    public class EntityFrameworkUnitOfWorkRegisteration : Shared.Infrastructure.UnitOfWork.EntityFramework.UnitOfWorkRegisteration<Context.TestContext>
    {
        private string _name;

        private Action<ContainerBuilder> _repositoryRegisteration;

        public override string Name => _name;

        public override Assembly[] EntityAssemblies => new Assembly[] { Assembly.Load(new AssemblyName("Shared.Infrastructure.Test")) };

        public override Assembly[] RepositoryAssemblies => new Assembly[] { Assembly.Load(new AssemblyName("Shared.Infrastructure.Test")) };

        public EntityFrameworkUnitOfWorkRegisteration(string name, Action<ContainerBuilder> repositoryRegisteration = null)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _repositoryRegisteration = repositoryRegisteration;
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder containerBuilder)
        {
            base.ConfigureContainerBuilder(containerBuilder);

            _repositoryRegisteration?.Invoke(containerBuilder);
        }
    }

    public class CassandraUnitOfWorkRegisteration : Shared.Infrastructure.UnitOfWork.Cassandra.UnitOfWorkRegisteration
    {
        private CassandraOptions _cassandraOptions;

        protected override CassandraOptions CassandraOptions => _cassandraOptions;

        public CassandraUnitOfWorkRegisteration(CassandraOptions cassandraOptions)
        {
            _cassandraOptions = cassandraOptions ?? throw new ArgumentNullException(nameof(cassandraOptions));
        }

        public override string Name => "cassandra";

        public override Assembly[] EntityAssemblies => new Assembly[] { Assembly.Load(new AssemblyName("Shared.Infrastructure.Test")) };

        public override Assembly[] RepositoryAssemblies => new Assembly[] { };
    }
}

