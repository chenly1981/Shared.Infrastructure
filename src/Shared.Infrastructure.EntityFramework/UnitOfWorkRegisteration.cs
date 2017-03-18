using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Shared.Infrastructure.UnitOfWork.EntityFramework;

namespace Shared.Infrastructure.EntityFramework
{
    public abstract class UnitOfWorkRegisteration<TContext> : UnitOfWork.UnitOfWorkRegisteration
        where TContext : DbContext
    {
        public override Type DefaultRepositoryType => typeof(RepositoryBase<>);

        public override Type UnitOfWorkCreatorType => typeof(EntityFrameworkUnitOfWorkCreator<TContext>);

        protected override void ConfigureContainerBuilder(ContainerBuilder containerBuilder)
        {
            base.ConfigureContainerBuilder(containerBuilder);
            
            containerBuilder.RegisterType<EntityFrameworkUnitOfWork<TContext>>();
        }
    }
}
