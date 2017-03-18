using Autofac;
using Autofac.Core;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.UnitOfWork;
using System;

namespace Shared.Infrastructure.UnitOfWork.EntityFramework
{
    internal class EntityFrameworkUnitOfWorkCreator<TContext> : UnitOfWorkCreator<EntityFrameworkUnitOfWork>
        where TContext : DbContext
    {
        public EntityFrameworkUnitOfWorkCreator(ILifetimeScope lifetimeScope)
            : base(lifetimeScope)
        {

        }

        public override IUnitOfWork CreateUnitOfWork()
        {
            var context = this.LifetimeScope.Resolve<TContext>();

            var childScope = this.LifetimeScope.BeginLifetimeScope(builder =>
            {
                builder.RegisterInstance(context).AsSelf().As<DbContext>().SingleInstance();
            });

            var uw = new EntityFrameworkUnitOfWork(childScope);
            return uw;
        }

        public override void Dispose()
        {
            
        }
    }
}
