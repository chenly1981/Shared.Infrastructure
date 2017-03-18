using Autofac;
using Autofac.Core;
using Microsoft.EntityFrameworkCore;
using System;
using Shared.Infrastructure.UnitOfWork;

namespace Shared.Infrastructure.UnitOfWork.EntityFramework
{
    public class EntityFrameworkUnitOfWork : UnitOfWorkBase
    {
        public DbContext Context { get; private set; }

        private ILifetimeScope LifetimeScope { get; set; }

        public EntityFrameworkUnitOfWork(ILifetimeScope lifetimeScope)
        {
            LifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(lifetimeScope));
            this.Context = lifetimeScope.Resolve<DbContext>();
        }

        public override ITransaction BeginTransaction()
        {
            var transaction = this.Context.Database.BeginTransaction();
            return new EntityFrameworkTransaction(transaction);
        }

        public override void Dispose()
        {
            base.Dispose();

            if (this.Context != null)
            {
                this.Context.Dispose();
            }
        }

        protected override T ResolveRepository<T>()
        {
            return this.LifetimeScope.Resolve<T>();
        }
    }
}
