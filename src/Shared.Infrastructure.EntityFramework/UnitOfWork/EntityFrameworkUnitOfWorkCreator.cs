using Autofac;
using Autofac.Core;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.UnitOfWork;
using System;

namespace Shared.Infrastructure.UnitOfWork.EntityFramework
{
    internal class EntityFrameworkUnitOfWorkCreator<TContext> : IUnitOfWorkCreator
        where TContext : DbContext
    {
        private IComponentContext ComponentContext { get; set; }

        public EntityFrameworkUnitOfWorkCreator(IComponentContext componentContext)
        {
            if (componentContext == null)
            {
                throw new ArgumentNullException(nameof(componentContext));
            }
            
            this.ComponentContext = componentContext;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            var uw = this.ComponentContext.Resolve<EntityFrameworkUnitOfWork<TContext>>();
            return uw;
        }

        public void Dispose()
        {
            
        }
    }
}
