using Autofac;
using System;

namespace Shared.Infrastructure.UnitOfWork
{
    public class UnitOfWorkProvider : IUnitOfWorkProvider
    {
        public IComponentContext ComponentContext { get; private set; }

        public UnitOfWorkProvider(IComponentContext componentContext)
        {
            ComponentContext = componentContext;
        }

        public IUnitOfWork CreateUnitOfWork(string alias)
        {
            string creatorName = UnitOfWorkHelper.GetUnitOfWorkName(alias);
            if (ComponentContext.IsRegisteredWithName<IUnitOfWorkCreator>(creatorName))
            {
                return ComponentContext.ResolveNamed<IUnitOfWorkCreator>(creatorName).CreateUnitOfWork();
            }
            else
            {
                throw new Exception("No UnitOfWork creator found");
            }
        }
    }
}
