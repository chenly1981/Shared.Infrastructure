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

        public IUnitOfWork CreateUnitOfWork(string name)
        {
            string creatorName = Consts.UNIT_OF_WORK_CREATOR_PREFIX + name;
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
