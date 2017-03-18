using Cassandra;
using Cassandra.Data.Linq;
using Cassandra.Mapping;
using Shared.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.UnitOfWork.Cassandra
{
    public class RepositoryBase<T> : AbstractRepositoryBase<T> 
        where T : class, IEntity
    {
        public RepositoryBase(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {

        }

        #region sync methods

        public override void Insert(T entity)
        {
            Table<T> table = this.GetTable();
            var statement = table.Insert(entity);

            Invoke(() =>
            {
                this.Connection.Execute(statement);
            });            
        }

        public override void InsertAll(IEnumerable<T> entityList)
        {
            var table = this.GetTable();

            var statements = entityList.Select(t => table.Insert(t)).ToArray();

            var batch = new BatchStatement();
            foreach (var statement in statements)
            {
                batch.Add(statement);
            }

            Invoke(() =>
            {
                this.Connection.Execute(batch);
            });
        }

        public override List<T> All()
        {
            var table = this.GetTable();
            List<T> result = null;

            Invoke(() =>
            {
                result = table.Execute().ToList();
            });

            return result;
        }

        public override List<T> Query(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var table = this.GetTable();

            List<T> result = null;

            Invoke(() =>
            {
                result = table.Where(predicate).Execute().ToList();
            });

            return result;
        }

        public override int Delete(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var table = this.GetTable();

            Invoke(() =>
            {
                table.DeleteIf(predicate).Execute();
            });

            return 0;
        }

        public override T Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var table = this.GetTable();

            T result = null;

            Invoke(() =>
            {
                result = table.FirstOrDefault(predicate).Execute();
            });

            return result;
        }

        public override void BatchInsert(IEnumerable<T> entityList)
        {
            var table = this.GetTable();

            entityList.BatchInvoke(items =>
            {
                var batch = new BatchStatement();
                foreach (var item in items)
                {
                    batch.Add(table.Insert(item));
                }

                Invoke(() =>
                {
                    this.Connection.Execute(batch);
                });                
            }, 1000);
        }

        #endregion

        #region async methods

        public async override Task InsertAsync(T entity)
        {
            var table = this.GetTable();

            var statement = table.Insert(entity);

            await this.Connection.ExecuteAsync(statement);
        }

        public async override Task InsertAllAsync(IEnumerable<T> entityList)
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

        public async override Task<List<T>> AllAsync()
        {
            IMapper mapper = new Mapper(this.Connection);
            var result = await mapper.FetchAsync<T>();
            return result.ToList();
        }        

        public async override Task BatchInsertAsync(IEnumerable<T> entityList)
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
    }
}
