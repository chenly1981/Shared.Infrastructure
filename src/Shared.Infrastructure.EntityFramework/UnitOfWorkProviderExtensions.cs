using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.UnitOfWork;
using Shared.Infrastructure.UnitOfWork.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace Shared.Infrastructure.UnitOfWork
{
    public static class UnitOfWorkProviderExtensions
    {
        public static IUnitOfWorkProvider AddEntityFramework<TContext>(this IUnitOfWorkProvider provider, string alias, Action<ContainerBuilder> repositoryRegisteration = null)
            where TContext : DbContext
        {
            provider.AddUnitOfWorkCreator<EntityFrameworkUnitOfWorkCreator<TContext>>(alias, builder =>
            {
                builder.RegisterGeneric(typeof(RepositoryBase<>));
                builder.RegisterType<EntityFrameworkUnitOfWork<TContext>>();
                repositoryRegisteration?.Invoke(builder);
            });

            return provider;
        }
    }
}
