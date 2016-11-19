using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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
                containerBuilder.AddUnitOfWork()
                    .AddEntityFramework<Context.TestContext>("ef");
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
    }
}
