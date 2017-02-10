using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.UnitOfWork
{
    public abstract class UnitOfWorkCreator<TUnitOfWork> : IUnitOfWorkCreator
        where TUnitOfWork : IUnitOfWork
    {        
        protected ILifetimeScope LifetimeScope { get; private set; }
        
        public UnitOfWorkCreator(ILifetimeScope lifetimeScope)
        {
            if (lifetimeScope == null)
            {
                throw new ArgumentNullException(nameof(lifetimeScope));
            }

            LifetimeScope = lifetimeScope;
        }

        public virtual IUnitOfWork CreateUnitOfWork()
        {
            return this.LifetimeScope.Resolve<TUnitOfWork>();
        }

        public virtual void Dispose()
        {
            if (LifetimeScope != null)
            {
                LifetimeScope.Dispose();
            }
        }
    }
}
