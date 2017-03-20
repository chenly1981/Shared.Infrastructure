using Autofac;
using Microsoft.EntityFrameworkCore;
using System;

namespace Shared.Infrastructure.UnitOfWork.EntityFramework
{
    public abstract class UnitOfWorkRegisteration<TContext> : UnitOfWorkRegisteration
        where TContext : DbContext
    {
        public override Type DefaultRepositoryType => typeof(RepositoryBase<>);

        public override Type UnitOfWorkCreatorType => typeof(EntityFrameworkUnitOfWorkCreator<TContext>);

        protected override void ConfigureContainerBuilder(ContainerBuilder containerBuilder)
        {
            base.ConfigureContainerBuilder(containerBuilder);
            
            containerBuilder.RegisterType<EntityFrameworkUnitOfWork>();
        }
    }
}
