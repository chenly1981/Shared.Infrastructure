using System;

namespace Shared.Infrastructure.UnitOfWork
{
    /// <summary>
    /// The UnitOfWork instance
    /// </summary>
    public interface IUnitOfWork: IDisposable
    {
        /// <summary>
        /// Create a custom repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T CreateRepository<T>() where T : IRepository;

        /// <summary>
        /// Create a transaction
        /// </summary>
        /// <returns></returns>
        ITransaction BeginTransaction();
    }
}
