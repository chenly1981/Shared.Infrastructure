using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Shared.Infrastructure
{
    public class InfrastructureFactory : IServiceProviderFactory<ContainerBuilder>
    {
        public ContainerBuilder CreateBuilder(IServiceCollection services)
        {
            services.AddSingleton(
                HtmlEncoder.Create(
                    allowedRanges: new[]
                    {
                        UnicodeRanges.BasicLatin,
                        UnicodeRanges.CjkUnifiedIdeographs
                    }
                )
            );
            services.AddOptions();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);         

            return containerBuilder;
        }

        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            return new AutofacServiceProvider(containerBuilder.Build());
        }
    }
}
