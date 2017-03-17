using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;

namespace Shared.Infrastructure.UnitOfWork
{
    internal class UnitOfWorkProvider : IUnitOfWorkProvider
    {
        private Dictionary<string, IUnitOfWorkCreator> UnitOfWorkCreators { get; set; }

        public ILifetimeScope LifetimeScope { get; private set; }

        private bool IsDisposed { get; set; }

        public UnitOfWorkProvider(ILifetimeScope lifetimeScope)
        {
            LifetimeScope = lifetimeScope;
            UnitOfWorkCreators = new Dictionary<string, IUnitOfWorkCreator>();
        }

        public IUnitOfWork CreateUnitOfWork(string alias)
        {
            if (IsDisposed)
            {
                throw new InvalidOperationException("UnitOfWork provider has been already disposed");
            }

            if (UnitOfWorkCreators.ContainsKey(alias))
            {
                return UnitOfWorkCreators[alias].CreateUnitOfWork();
            }
            else
            {
                throw new Exception("No UnitOfWork creator found");
            }
        }

        public void Register(IUnitOfWorkRegisteration unitOfWorkRegisteration)
        {
            if (IsDisposed)
            {
                throw new InvalidOperationException("UnitOfWork provider has been already disposed");
            }

            lock (UnitOfWorkCreators)
            {
                ILifetimeScope childScope = LifetimeScope.BeginLifetimeScope(containerBuilder =>
                {
                    containerBuilder.RegisterType(unitOfWorkRegisteration.UnitOfWorkCreatorType)
                        .As<IUnitOfWorkCreator>()
                        .SingleInstance();
                    unitOfWorkRegisteration.Initialize(containerBuilder);
                });

                IUnitOfWorkCreator unitOfWorkCreator = childScope.Resolve<IUnitOfWorkCreator>(TypedParameter.From(childScope));

                UnitOfWorkCreators.Add(unitOfWorkRegisteration.Name, unitOfWorkCreator);
            }
        }

        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;
            lock (UnitOfWorkCreators)
            {
                foreach (KeyValuePair<string, IUnitOfWorkCreator> pair in UnitOfWorkCreators)
                {
                    pair.Value?.Dispose();
                }

                UnitOfWorkCreators.Clear();
            }
        }
    }
}
