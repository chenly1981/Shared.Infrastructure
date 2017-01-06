using Autofac;
using System;

namespace Shared.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkProvider : IDisposable
    {
        IUnitOfWork CreateUnitOfWork(string name);

        void AddUnitOfWorkCreator<TUnitOfWorkCreator>(string alias, Action<ContainerBuilder> repositoryRegisteration = null) where TUnitOfWorkCreator : IUnitOfWorkCreator;
    }
}
