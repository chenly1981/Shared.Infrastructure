using Autofac;
using Autofac.Core;
using Cassandra;
using Shared.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.UnitOfWork.Cassandra
{
    public class CassandraUnitOfWorkCreator : IUnitOfWorkCreator
    {
        private CassandraOptions CassandraOptions { get; set; }

        private ICluster CassandraCluster { get; set; }

        private IComponentContext ComponentContext { get; set; }

        public CassandraUnitOfWorkCreator(CassandraOptions cassandraOptions, IComponentContext componentContext)
        {
            this.ComponentContext = componentContext;
            this.CassandraOptions = cassandraOptions;

            var queryOptions = new QueryOptions();
            queryOptions.SetConsistencyLevel(ConsistencyLevel.One);

            this.CassandraCluster = Cluster.Builder()
                .AddContactPoints(this.CassandraOptions.Nodes)
                .WithCredentials(this.CassandraOptions.User, this.CassandraOptions.Password)
                .WithQueryOptions(queryOptions)
                .Build();
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            var session = this.CassandraCluster.Connect(this.CassandraOptions.KeySpace);

            Parameter parameter = TypedParameter.From(session);

            return this.ComponentContext.Resolve<CassandraUnitOfWork>(parameter);
        }
    }
}
