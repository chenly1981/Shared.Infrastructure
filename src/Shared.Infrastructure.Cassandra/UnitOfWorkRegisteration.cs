using Shared.Infrastructure.UnitOfWork.Cassandra;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Shared.Infrastructure.Cassandra
{
    public abstract class UnitOfWorkRegisteration : UnitOfWork.UnitOfWorkRegisteration
    {
        public override Type DefaultRepositoryType => typeof(RepositoryBase<>);

        public override Type UnitOfWorkCreatorType => typeof(CassandraUnitOfWorkCreator);

        private CassandraOptions CassandraOptions { get; set; }

        public UnitOfWorkRegisteration(CassandraOptions cassandraOptions)
        {
            CassandraOptions = cassandraOptions ?? throw new ArgumentNullException(nameof(cassandraOptions));
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder containerBuilder)
        {
            base.ConfigureContainerBuilder(containerBuilder);

            containerBuilder.RegisterInstance(this.CassandraOptions).SingleInstance();
            containerBuilder.RegisterType<CassandraUnitOfWork>();
        }
    }
}
