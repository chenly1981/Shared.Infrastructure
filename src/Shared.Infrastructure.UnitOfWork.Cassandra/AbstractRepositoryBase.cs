using Cassandra;
using Cassandra.Data.Linq;
using Cassandra.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace Shared.Infrastructure.UnitOfWork.Cassandra
{
    public abstract class AbstractRepositoryBase<T> : IRepository<T>
        where T : class, IEntity
    {
        protected ISession Connection
        {
            get
            {
                return ServiceProvider.GetService<ISession>();
            }
        }

        private ILoggerFactory LoggerFactory { get; set; }

        protected IServiceProvider ServiceProvider { get; private set; }

        protected ILogger Logger { get; set; }

        public AbstractRepositoryBase(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            LoggerFactory = serviceProvider.GetService<ILoggerFactory>();
            Logger = LoggerFactory.CreateLogger(this.GetType());
        }

        #region protected methods

        protected Table<T> GetTable()
        {
            return new Table<T>(this.Connection);
        }

        protected List<T> QueryPaged(System.Linq.Expressions.Expression<Func<T, bool>> predicate, int pageIndex, int pageSize)
        {
            int currentPageIndex = 0;

            byte[] pagingState = null;
            IPage<T> pagedResult = null;

            while (currentPageIndex < pageIndex)
            {
                var query = this.GetTable()
                    .SetPageSize(pageSize);

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                if (pagingState != null)
                {
                    query.SetPagingState(pagingState);
                }

                Invoke(() =>
                {
                    pagedResult = query.ExecutePaged();
                });
                pagingState = pagedResult.PagingState;

                currentPageIndex++;
            }

            return pagedResult.ToList();
        }

        protected void Invoke(Action action)
        {
            bool isSuccess = false;
            int tryCount = 0;
            do
            {
                try
                {
                    tryCount++;
                    action();
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    if (tryCount < 5)
                    {
                        Logger.LogError(0, ex, "Cassandra Operation Error, try count:" + tryCount + ", will retry. Error Message: " + ex.Message);
                    }
                    else
                    {
                        throw;
                    }
                }
            } while (!isSuccess);
        }

        protected void ExecuteCql(string cql)
        {
            Invoke(() =>
            {
                this.Connection.Execute(cql);
            });
        }

        #endregion

        public virtual List<T> All()
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<T>> AllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual void BatchInsert(IEnumerable<T> entityList)
        {
            throw new NotImplementedException();
        }

        public virtual Task BatchInsertAsync(IEnumerable<T> entityList)
        {
            throw new NotImplementedException();
        }

        public virtual List<TProperty> Column<TProperty>(Expression<Func<T, bool>> predicate, Expression<Func<T, TProperty>> propertySelector)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<TProperty>> ColumnAsync<TProperty>(Expression<Func<T, bool>> predicate, Expression<Func<T, TProperty>> propertySelector)
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

        public virtual Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<long> CountAsync(Expression<Func<T, bool>> predicate)
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

        public virtual Task<int> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> DeleteAsync(params T[] entityList)
        {
            throw new NotImplementedException();
        }

        public virtual bool Exist(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> ExistAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void InsertAll(IEnumerable<T> entityList)
        {
            throw new NotImplementedException();
        }

        public virtual Task InsertAllAsync(IEnumerable<T> entityList)
        {
            throw new NotImplementedException();
        }

        public virtual long InsertAndReturnIdentity(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<long> InsertAndReturnIdentityAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task InsertAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> Query(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<T>> QueryAsync(Expression<Func<T, bool>> predicate)
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

        public virtual Task<int> UpdateAsync(object updateOnly, Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
