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
                    provider.AddEntityFramework<Context.TestContext>("ef");
                });
            });

            IUnitOfWorkProvider unitOfWirkProvider = serviceProvider.GetService<IUnitOfWorkProvider>();
            using (IUnitOfWork uw = unitOfWirkProvider.CreateUnitOfWork("ef"))
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
                Assert.AreEqual(entity.Name, "name");
            }
        }

        [TestMethod]
        public void AddCassandra()
        {
            IServiceProvider serviceProvider = InitDependencyInjection(services =>
            {

            }, containerBuilder =>
            {
                containerBuilder.AddUnitOfWork(provider =>
                {
                    provider.AddCassandra("cassandra", new UnitOfWork.Cassandra.CassandraOptions
                    {
                        Nodes = new string[] { "192.168.40.229" },
                        User = "cassandra",
                        Password = "cassandra",
                        KeySpace = "ias_dev"
                    }, builder =>
                    {

                    });
                });
            });

            IUnitOfWorkProvider unitOfWirkProvider = serviceProvider.GetService<IUnitOfWorkProvider>();
            using (IUnitOfWork uw = unitOfWirkProvider.CreateUnitOfWork("cassandra"))
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
                services.AddDbContext<Context.TestContext>(builder =>
                {
                    builder.UseInMemoryDatabase();
                }, ServiceLifetime.Transient);
            }, containerBuilder =>
            {
                containerBuilder.AddUnitOfWork(provider =>
                {
                    provider.AddEntityFramework<Context.TestContext>("ef1", builder =>
                    {
                        builder.RegisterType<Repository.EntityFramework.TestEntityRepository>().As<Repository.Interface.ITestEntityRepository>();
                    });

                    provider.AddEntityFramework<Context.TestContext>("ef2");
                });
            });

            IUnitOfWorkProvider unitOfWirkProvider = serviceProvider.GetService<IUnitOfWorkProvider>();
            using (IUnitOfWork uw = unitOfWirkProvider.CreateUnitOfWork("ef1"))
            {
                Repository.Interface.ITestEntityRepository repository = uw.CreateRepository<Repository.Interface.ITestEntityRepository>();
                Assert.IsNotNull(repository);

                var entityList = repository.All();
            }

            using (IUnitOfWork uw = unitOfWirkProvider.CreateUnitOfWork("ef2"))
            {
                try
                {
                    Repository.Interface.ITestEntityRepository repository = uw.CreateRepository<Repository.Interface.ITestEntityRepository>();
                    Assert.IsNull(repository);
                }
                catch
                {

                }
            }
        }
    }
}

