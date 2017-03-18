using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shared.Infrastructure.UnitOfWork
{
    public static class UnitOfWorkExtensions
    {
        #region sync methods

        public static void Insert<T>(this IUnitOfWork uw, T entity)
            where T : class, IEntity
        {
            uw.CreateRepository<IRepository<T>>().Insert(entity);
        }

        public static long InsertAndReturnIdentity<T>(this IUnitOfWork uw, T entity)
            where T : class, IEntity
        {
            return uw.CreateRepository<IRepository<T>>().InsertAndReturnIdentity(entity);
        }

        public static void InsertAll<T>(this IUnitOfWork uw, IEnumerable<T> entityList)
            where T : class, IEntity
        {
            uw.CreateRepository<IRepository<T>>().InsertAll(entityList);
        }

        public static List<T> All<T>(this IUnitOfWork uw)
            where T : class, IEntity
        {
            return uw.CreateRepository<IRepository<T>>().All();
        }

        public static List<T> Query<T>(this IUnitOfWork uw, Expression<Func<T, bool>> predicate)
            where T : class, IEntity
        {
            return uw.CreateRepository<IRepository<T>>().Query(predicate);
        }

        public static int Delete<T>(this IUnitOfWork uw, Expression<Func<T, bool>> predicate)
            where T : class, IEntity
        {
            return uw.CreateRepository<IRepository<T>>().Delete(predicate);
        }

        public static int Delete<T>(this IUnitOfWork uw, params T[] entityList)
            where T : class, IEntity
        {
            return uw.CreateRepository<IRepository<T>>().Delete(entityList);
        }

        public static T Get<T>(this IUnitOfWork uw, Expression<Func<T, bool>> predicate)
            where T : class, IEntity
        {
            return uw.CreateRepository<IRepository<T>>().Get(predicate);
        }

        public static int Update<T>(this IUnitOfWork uw, object updateOnly, Expression<Func<T, bool>> predicate)
            where T : class, IEntity
        {
            return uw.CreateRepository<IRepository<T>>().Update(updateOnly, predicate);
        }

        public static int Update<T>(this IUnitOfWork uw, T entity)
            where T : class, IEntity
        {
            return uw.CreateRepository<IRepository<T>>().Update(entity);
        }

        public static bool Exist<T>(this IUnitOfWork uw, Expression<Func<T, bool>> predicate)
            where T : class, IEntity
        {
            return uw.CreateRepository<IRepository<T>>().Exist(predicate);
        }

        public static List<TProperty> Column<T, TProperty>(this IUnitOfWork uw, Expression<Func<T, bool>> predicate, Expression<Func<T, TProperty>> propertySelector)
            where T : class, IEntity
        {
            return uw.CreateRepository<IRepository<T>>().Column(predicate, propertySelector);
        }

        public static long Count<T>(this IUnitOfWork uw)
            where T : class, IEntity
        {
            return uw.CreateRepository<IRepository<T>>().Count();
        }

        public static long Count<T>(this IUnitOfWork uw, Expression<Func<T, bool>> predicate)
            where T : class, IEntity
        {
            return uw.CreateRepository<IRepository<T>>().Count(predicate);
        }

        public static void BatchInsert<T>(this IUnitOfWork uw, IEnumerable<T> entityList)
            where T : class, IEntity
        {
            uw.CreateRepository<IRepository<T>>().BatchInsert(entityList);
        }

        #endregion

        #region async methods

        public static async Task InsertAsync<T>(this IUnitOfWork uw, T entity)
            where T : class, IEntity
        {
            await uw.CreateRepository<IRepository<T>>().InsertAsync(entity);
        }

        public static async Task<long> InsertAndReturnIdentityAsync<T>(this IUnitOfWork uw, T entity)
            where T : class, IEntity
        {
            return await uw.CreateRepository<IRepository<T>>().InsertAndReturnIdentityAsync(entity);
        }

        public static async Task InsertAllAsync<T>(this IUnitOfWork uw, IEnumerable<T> entityList)
            where T : class, IEntity
        {
            await uw.CreateRepository<IRepository<T>>().InsertAllAsync(entityList);
        }

        public static async Task<List<T>> AllAsync<T>(this IUnitOfWork uw)
            where T : class, IEntity
        {
            return await uw.CreateRepository<IRepository<T>>().AllAsync();
        }

        public static async Task<List<T>> QueryAsync<T>(this IUnitOfWork uw, Expression<Func<T, bool>> predicate)
            where T : class, IEntity
        {
            return await uw.CreateRepository<IRepository<T>>().QueryAsync(predicate);
        }

        public static async Task<int> DeleteAsync<T>(this IUnitOfWork uw, Expression<Func<T, bool>> predicate)
            where T : class, IEntity
        {
            return await uw.CreateRepository<IRepository<T>>().DeleteAsync(predicate);
        }

        public static async Task<int> DeleteAsync<T>(this IUnitOfWork uw, params T[] entityList)
            where T : class, IEntity
        {
            return await uw.CreateRepository<IRepository<T>>().DeleteAsync(entityList);
        }

        public static async Task<T> GetAsync<T>(this IUnitOfWork uw, Expression<Func<T, bool>> predicate)
            where T : class, IEntity
        {
            return await uw.CreateRepository<IRepository<T>>().GetAsync(predicate);
        }

        public static async Task<int> UpdateAsync<T>(this IUnitOfWork uw, object updateOnly, Expression<Func<T, bool>> predicate)
            where T : class, IEntity
        {
            return await uw.CreateRepository<IRepository<T>>().UpdateAsync(updateOnly, predicate);
        }

        public static async Task<int> UpdateAsync<T>(this IUnitOfWork uw, T entity)
            where T : class, IEntity
        {
            return await uw.CreateRepository<IRepository<T>>().UpdateAsync(entity);
        }

        public static async Task<bool> ExistAsync<T>(this IUnitOfWork uw, Expression<Func<T, bool>> predicate)
            where T : class, IEntity
        {
            return await uw.CreateRepository<IRepository<T>>().ExistAsync(predicate);
        }

        public static async Task<List<TProperty>> ColumnAsync<T, TProperty>(this IUnitOfWork uw, Expression<Func<T, bool>> predicate, Expression<Func<T, TProperty>> propertySelector)
            where T : class, IEntity
        {
            return await uw.CreateRepository<IRepository<T>>().ColumnAsync(predicate, propertySelector);
        }

        public static async Task<long> CountAsync<T>(this IUnitOfWork uw)
            where T : class, IEntity
        {
            return await uw.CreateRepository<IRepository<T>>().CountAsync();
        }

        public static async Task<long> CountAsync<T>(this IUnitOfWork uw, Expression<Func<T, bool>> predicate)
            where T : class, IEntity
        {
            return await uw.CreateRepository<IRepository<T>>().CountAsync(predicate);
        }

        public static async Task BatchInsertAsync<T>(this IUnitOfWork uw, IEnumerable<T> entityList)
            where T : class, IEntity
        {
            await uw.CreateRepository<IRepository<T>>().BatchInsertAsync(entityList);
        }

        #endregion
    }
}
