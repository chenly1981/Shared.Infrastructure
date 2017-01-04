using Autofac;
using Microsoft.Extensions.Options;
using Shared.Infrastructure.UnitOfWork;
using Shared.Infrastructure.UnitOfWork.Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// Add Cassandra support for UnitOfWork
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="builder"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static ContainerBuilder AddCassandra(this ContainerBuilder builder, string alias, CassandraOptions options)
        {
            string creatorName = UnitOfWorkHelper.GetUnitOfWorkName(alias);
            builder.RegisterGeneric(typeof(RepositoryBase<>));
            builder.RegisterType<CassandraUnitOfWork>();
            builder.RegisterType<CassandraUnitOfWorkCreator>()
                .WithParameter(TypedParameter.From(options))
                .Named<IUnitOfWorkCreator>(creatorName)
                .SingleInstance();

            return builder;
        }
    }
}
