using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shared.Infrastructure.UnitOfWork.EntityFramework
{
    public abstract class AbstractRepositoryBase<T> : IRepository<T>
        where T : class, IEntity
    {
        protected DbContext Context { get; private set; }

        protected DbSet<T> Set
        {
            get
            {
                return Context.Set<T>();
            }
        }

        public AbstractRepositoryBase(DbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual long InsertAndReturnIdentity(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void InsertAll(IEnumerable<T> entityList)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> All()
        {
            throw new NotImplementedException();
        }

        public virtual List<T> Query(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual int Delete(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual int Delete(params T[] entityList)
        {
            throw new NotImplementedException();
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual int Update(object updateOnly, Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual int Update(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual bool Exist(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual List<TProperty> Column<TProperty>(Expression<Func<T, bool>> predicate, Expression<Func<T, TProperty>> propertySelector)
        {
            throw new NotImplementedException();
        }

        public virtual long Count()
        {
            throw new NotImplementedException();
        }

        public virtual long Count(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual void BatchInsert(IEnumerable<T> entityList)
        {
            throw new NotImplementedException();
        }

        public virtual Task InsertAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<long> InsertAndReturnIdentityAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task InsertAllAsync(IEnumerable<T> entityList)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<T>> AllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<T>> QueryAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> DeleteAsync(params T[] entityList)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> UpdateAsync(object updateOnly, Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> ExistAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<TProperty>> ColumnAsync<TProperty>(Expression<Func<T, bool>> predicate, Expression<Func<T, TProperty>> propertySelector)
        {
            throw new NotImplementedException();
        }

        public virtual Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<long> CountAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task BatchInsertAsync(IEnumerable<T> entityList)
        {
            throw new NotImplementedException();
        }
    }
}
