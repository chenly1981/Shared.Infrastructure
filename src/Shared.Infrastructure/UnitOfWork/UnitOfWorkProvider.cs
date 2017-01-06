using Autofac;
using System;
using System.Collections.Generic;

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

        public void AddUnitOfWorkCreator<TUnitOfWorkCreator>(string alias, Action<ContainerBuilder> repositoryRegisteration) where TUnitOfWorkCreator : IUnitOfWorkCreator
        {
            if (IsDisposed)
            {
                throw new InvalidOperationException("UnitOfWork provider has been already disposed");
            }

            if (string.IsNullOrWhiteSpace(alias))
            {
                throw new ArgumentNullException(nameof(alias));
            }

            lock (UnitOfWorkCreators)
            {
                if (UnitOfWorkCreators.ContainsKey(alias))
                {
                    throw new InvalidOperationException($"You have already add a UnitOfWork creator with name '{alias}'");
                }

                ILifetimeScope childScope = LifetimeScope.BeginLifetimeScope(containerBuilder =>
                {
                    containerBuilder.RegisterType<TUnitOfWorkCreator>()
                        .As<IUnitOfWorkCreator>()
                        .SingleInstance();

                    repositoryRegisteration?.Invoke(containerBuilder);
                });

                IUnitOfWorkCreator unitOfWorkCreator = childScope.Resolve<IUnitOfWorkCreator>(TypedParameter.From(childScope));

                UnitOfWorkCreators.Add(alias, unitOfWorkCreator);
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
