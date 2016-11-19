using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;

namespace Shared.Infrastructure.Test
{
    [TestClass]
    public abstract class UnitTestBase : IDisposable
    {
        public void Dispose()
        {
            
        }

        protected IServiceProvider InitDependencyInjection(Action<IServiceCollection> servicesAction = null, Action<ContainerBuilder> containerBuilderAction = null)
        {
            var services = new ServiceCollection();
            servicesAction?.Invoke(services);

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            containerBuilderAction?.Invoke(containerBuilder);

            return new AutofacServiceProvider(containerBuilder.Build());
        }
    }
}
