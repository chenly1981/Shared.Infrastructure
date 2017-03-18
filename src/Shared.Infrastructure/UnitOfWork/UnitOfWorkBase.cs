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

        public abstract ITransaction BeginTransaction();

        protected abstract T ResolveRepository<T>() where T : IRepository;
    }
}
