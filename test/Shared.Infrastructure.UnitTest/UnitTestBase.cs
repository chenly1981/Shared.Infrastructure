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
        public virtual void Dispose()
        {
            
        }

        protected IServiceProvider InitDependencyInjection(Action<IServiceCollection> servicesAction = null, Action<ContainerBuilder> containerBuilderAction = null)
        {
            IServiceProviderFactory<ContainerBuilder> factory = new InfrastructureFactory();            

            var services = new ServiceCollection();
            servicesAction?.Invoke(services);

            ContainerBuilder containerBuilder = factory.CreateBuilder(services);            
            containerBuilderAction?.Invoke(containerBuilder);

            return factory.CreateServiceProvider(containerBuilder);
        }
    }
}
