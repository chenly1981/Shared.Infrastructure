using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
