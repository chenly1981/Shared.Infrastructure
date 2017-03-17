using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Shared.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkRegisteration
    {
        string Name { get; }

        Type UnitOfWorkCreatorType { get; }

        void Initialize(ContainerBuilder containerBuilder);
    }
}
