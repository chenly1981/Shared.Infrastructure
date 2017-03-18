using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shared.Infrastructure.UnitOfWork.EntityFramework
{
    /// <summary>
    /// 用于EntityFramework的仓储基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryBase<T> : AbstractRepositoryBase<T>
        where T : class, IEntity
    {
        public RepositoryBase(DbContext context)
            : base(context)
        {

        }

        #region sync methods

        public override void Insert(T entity)
        {            
            this.Set.Add(entity);
            this.Context.SaveChanges();
        }

        public override void InsertAll(IEnumerable<T> entityList)
        {
            this.Set.AddRange(entityList);
            this.Context.SaveChanges();
        }

        public override List<T> All()
        {
            return this.Context.Set<T>().ToList();
        }

        public override List<T> Query(Expression<Func<T, bool>> predicate)
        {
            return this.Context.Set<T>().Where(predicate).ToList();
        }

        public override int Delete(Expression<Func<T, bool>> predicate)
        {
            var entityList = this.Set.Where(predicate).ToList();
            this.Set.RemoveRange(entityList);
            return this.Context.SaveChanges();
        }

        public override int Delete(params T[] entityList)
        {
            this.Set.RemoveRange(entityList);
            return this.Context.SaveChanges();
        }

        public override T Get(Expression<Func<T, bool>> predicate)
        {
            return this.Set.Where(predicate).FirstOrDefault();
        }

        public override bool Exist(Expression<Func<T, bool>> predicate)
        {
            return this.Set.Any(predicate);
        }

        public override int Update(T entity)
        {
            this.Set.Update(entity);
            return this.Context.SaveChanges();
        }

        public override List<TProperty> Column<TProperty>(Expression<Func<T, bool>> predicate, Expression<Func<T, TProperty>> propertySelector)
        {
            return this.Set.Where(predicate).Select(propertySelector).ToList();
        }

        public override long Count()
        {
            return this.Set.LongCount();
        }

        public override long Count(Expression<Func<T, bool>> predicate)
        {
            return this.Set.LongCount(predicate);
        }

        #endregion

        #region async methods

        public override async Task InsertAsync(T entity)
        {
            this.Set.Add(entity);

            await this.Context.SaveChangesAsync();
        }

        public override async Task InsertAllAsync(IEnumerable<T> entityList)
        {
            this.Set.AddRange(entityList);
            await this.Context.SaveChangesAsync();
        }

        public override async Task<List<T>> AllAsync()
        {
            return await this.Set.ToListAsync();
        }

        public override async Task<List<T>> QueryAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.Set.Where(predicate).ToListAsync();
        }

        public override async Task<int> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var entityList = this.Set.Where(predicate).ToList();
            this.Set.RemoveRange(entityList);
            return await this.Context.SaveChangesAsync();
        }

        public override async Task<int> DeleteAsync(params T[] entityList)
        {
            this.Set.RemoveRange(entityList);
            return await this.Context.SaveChangesAsync();
        }

        public override async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.Set.FirstOrDefaultAsync(predicate);
        }

        public override async Task<int> UpdateAsync(T entity)
        {
            this.Set.Update(entity);
            return await this.Context.SaveChangesAsync();
        }

        public override async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.Set.AnyAsync(predicate);
        }

        public override async Task<List<TProperty>> ColumnAsync<TProperty>(Expression<Func<T, bool>> predicate, Expression<Func<T, TProperty>> propertySelector)
        {
            var query = this.Set.Where(predicate).Select(propertySelector);

            return await query.ToListAsync();
        }

        public override async Task<long> CountAsync()
        {
            return await this.Set.LongCountAsync();
        }

        public override async Task<long> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.Set.LongCountAsync(predicate);
        }

        #endregion
    }
}
