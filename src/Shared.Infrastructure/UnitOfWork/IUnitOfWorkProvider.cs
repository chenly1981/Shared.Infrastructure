using Autofac;
using System;

namespace Shared.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkProvider : IDisposable
    {
        IUnitOfWork CreateUnitOfWork(string name);

        void Register(IUnitOfWorkRegisteration unitOfWorkRegisteration);
    }
}
