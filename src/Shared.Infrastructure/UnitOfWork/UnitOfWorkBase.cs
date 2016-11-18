using System;
using System.Collections.Generic;

namespace Shared.Infrastructure.UnitOfWork
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        private Dictionary<Type, object> Repositories { get; set; }

        public UnitOfWorkBase()
        {
            Repositories = new Dictionary<Type, object>();
        }

        public T CreateRepository<T>()
            where T : IRepository
        {
            Type repositoryType = typeof(T);

            if (Repositories.ContainsKey(repositoryType))
            {
                return (T)Repositories[repositoryType];
            }
            else
            {
                T repository = ResolveRepository<T>();

                Repositories.Add(repositoryType, repository);

                return repository;
            }
        }

        public virtual void Dispose()
        {
            if (Repositories != null)
            {
                Repositories.Clear();
            }
        }

        public IRepository<T> CreateDefaultRepository<T>() where T : class, IEntity
        {
            Type repositoryType = typeof(IRepository<T>);

            if (Repositories.ContainsKey(repositoryType))
            {
                return (IRepository<T>)Repositories[repositoryType];
            }
            else
            {
                IRepository<T> repository = ResolveDefaultRepository<T>();
                Repositories.Add(repositoryType, repository);

                return repository;
            }
        }

        public abstract ITransaction BeginTransaction();

        protected abstract T ResolveRepository<T>() where T : IRepository;

        protected abstract IRepository<T> ResolveDefaultRepository<T>() where T : class, IEntity;
    }
}
