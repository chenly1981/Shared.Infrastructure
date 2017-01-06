using Autofac;
using Autofac.Core;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.UnitOfWork;
using System;

namespace Shared.Infrastructure.UnitOfWork.EntityFramework
{
    internal class EntityFrameworkUnitOfWorkCreator<TContext> : UnitOfWorkCreator<EntityFrameworkUnitOfWork<TContext>>
        where TContext : DbContext
    {
        public EntityFrameworkUnitOfWorkCreator(ILifetimeScope lifetimeScope)
            : base(lifetimeScope)
        {

        }

        public IUnitOfWork CreateUnitOfWork()
        {
            var uw = this.LifetimeScope.Resolve<EntityFrameworkUnitOfWork<TContext>>();
            return uw;
        }

        public void Dispose()
        {
            
        }
    }
}
