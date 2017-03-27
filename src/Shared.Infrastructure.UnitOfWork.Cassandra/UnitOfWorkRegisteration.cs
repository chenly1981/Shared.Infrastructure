﻿using Shared.Infrastructure.UnitOfWork.Cassandra;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Shared.Infrastructure.UnitOfWork.Cassandra
{
    public abstract class UnitOfWorkRegisteration : UnitOfWork.UnitOfWorkRegisteration
    {
        public override Type DefaultRepositoryType => typeof(RepositoryBase<>);

        public override Type UnitOfWorkCreatorType => typeof(CassandraUnitOfWorkCreator);

        protected abstract CassandraOptions CassandraOptions { get; }

        public UnitOfWorkRegisteration()
        {
            
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder containerBuilder)
        {
            base.ConfigureContainerBuilder(containerBuilder);

            containerBuilder.RegisterInstance(this.CassandraOptions).SingleInstance();
            containerBuilder.RegisterType<CassandraUnitOfWork>();
        }
    }
}
