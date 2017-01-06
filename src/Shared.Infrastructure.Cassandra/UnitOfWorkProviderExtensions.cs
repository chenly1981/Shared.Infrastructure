using Autofac;
using Shared.Infrastructure.UnitOfWork;
using Shared.Infrastructure.UnitOfWork.Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.UnitOfWork
{
    public static class UnitOfWorkProviderExtensions
    {
        public static IUnitOfWorkProvider AddCassandra(this IUnitOfWorkProvider provider, string alias, CassandraOptions options, Action<ContainerBuilder> repositoryRegisteration)
        {
            provider.AddUnitOfWorkCreator<CassandraUnitOfWorkCreator>(alias, builder =>
            {
                builder.RegisterGeneric(typeof(RepositoryBase<>));
                builder.RegisterType<CassandraUnitOfWork>();
                builder.RegisterInstance(options);
                repositoryRegisteration?.Invoke(builder);
            });

            return provider;
        }
    }
}
