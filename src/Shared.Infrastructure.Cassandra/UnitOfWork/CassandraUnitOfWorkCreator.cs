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
    public class CassandraUnitOfWorkCreator : UnitOfWorkCreator<CassandraUnitOfWork>
    {
        private CassandraOptions CassandraOptions { get; set; }

        private ICluster CassandraCluster { get; set; }

        public CassandraUnitOfWorkCreator(ILifetimeScope lifetimeScope, CassandraOptions cassandraOptions)
            : base(lifetimeScope)
        {
            this.CassandraOptions = cassandraOptions;

            var queryOptions = new QueryOptions();
            queryOptions.SetConsistencyLevel(ConsistencyLevel.One);

            this.CassandraCluster = Cluster.Builder()
                .AddContactPoints(this.CassandraOptions.Nodes)
                .WithCredentials(this.CassandraOptions.User, this.CassandraOptions.Password)
                .WithQueryOptions(queryOptions)
                .Build();
        }

        public override IUnitOfWork CreateUnitOfWork()
        {
            var session = this.CassandraCluster.Connect(this.CassandraOptions.KeySpace);

            var childScope = this.LifetimeScope.BeginLifetimeScope(builder =>
            {
                builder.RegisterInstance(session).As<ISession>().SingleInstance();
            });
                        
            return new CassandraUnitOfWork(childScope);
        }

        public override void Dispose()
        {
            if (this.CassandraCluster != null)
            {
                this.CassandraCluster.Dispose();
            }
        }
    }
}
