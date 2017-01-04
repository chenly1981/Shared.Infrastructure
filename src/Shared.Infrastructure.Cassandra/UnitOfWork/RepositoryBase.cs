using Cassandra;
using Cassandra.Data.Linq;
using Cassandra.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Infrastructure.Extensions;

namespace Shared.Infrastructure.UnitOfWork.Cassandra
{
    public class RepositoryBase<T> : IRepository<T> where T : class, IEntity
    {
        protected ISession Connection { get; private set; }

        public RepositoryBase(ISession connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            this.Connection = connection;
        }

        #region sync methods

        public void Insert(T entity)
        {
            Table<T> table = this.GetTable();
            var statement = table.Insert(entity);

            this.Connection.Execute(statement);
        }

        public long InsertAndReturnIdentity(T entity)
        {
            throw new NotImplementedException();
        }

        public void InsertAll(IEnumerable<T> entityList)
        {
            var table = this.GetTable();

            var statements = entityList.Select(t => table.Insert(t)).ToArray();

            var batch = new BatchStatement();
            foreach (var statement in statements)
            {
                batch.Add(statement);
            }

            this.Connection.Execute(batch);
        }

        public List<T> All()
        {
            var table = this.GetTable();
            return table.Execute().ToList();
        }

        public List<T> Query(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var table = this.GetTable();

            return table.Where(predicate).Execute().ToList();
        }

        public int Delete(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var table = this.GetTable();

            table.DeleteIf(predicate).Execute();

            return 0;
        }

        public int Delete(params T[] entityList)
        {
            throw new NotImplementedException();
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var table = this.GetTable();

            return table.FirstOrDefault(predicate).Execute();
        }

        public int Update(object updateOnly, System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public int Update(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Exist(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<TProperty> Column<TProperty>(System.Linq.Expressions.Expression<Func<T, bool>> predicate, System.Linq.Expressions.Expression<Func<T, TProperty>> propertySelector)
        {
            throw new NotImplementedException();
        }

        public long Count()
        {
            throw new NotImplementedException();
        }

        public long Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void BatchInsert(IEnumerable<T> entityList)
        {
            var table = this.GetTable();

            entityList.BatchInvoke(items =>
            {
                var batch = new BatchStatement();
                foreach (var item in items)
                {
                    batch.Add(table.Insert(item));
                }
                this.Connection.Execute(batch);
            }, 1000);
        }

        #endregion

        #region async methods

        public async Task InsertAsync(T entity)
        {
            var table = this.GetTable();

            var statement = table.Insert(entity);

            await this.Connection.ExecuteAsync(statement);
        }

        public Task<long> InsertAndReturnIdentityAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAllAsync(IEnumerable<T> entityList)
        {
            var table = this.GetTable();

            var statements = entityList.Select(t => table.Insert(t)).ToArray();

            var batch = new BatchStatement();
            foreach (var statement in statements)
            {
                batch.Add(statement);
            }

            await this.Connection.ExecuteAsync(batch);
        }

        public async Task<List<T>> AllAsync()
        {
            IMapper mapper = new Mapper(this.Connection);
            var result = await mapper.FetchAsync<T>();
            return result.ToList();
        }

        public Task<List<T>> QueryAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(params T[] entityList)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(object updateOnly, System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<TProperty>> ColumnAsync<TProperty>(System.Linq.Expressions.Expression<Func<T, bool>> predicate, System.Linq.Expressions.Expression<Func<T, TProperty>> propertySelector)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task BatchInsertAsync(IEnumerable<T> entityList)
        {
            var table = this.GetTable();

            List<Task> tasks = new List<Task>();
            entityList.BatchInvoke(items =>
            {
                var batch = new BatchStatement();
                foreach (var item in items)
                {
                    batch.Add(table.Insert(item));
                }
                tasks.Add(this.Connection.ExecuteAsync(batch));
            }, 1000);

            await Task.WhenAll(tasks);
        }

        #endregion

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

                pagedResult = query.ExecutePaged();
                pagingState = pagedResult.PagingState;

                currentPageIndex++;
            }

            return pagedResult.ToList();
        }

        #endregion
    }
}
