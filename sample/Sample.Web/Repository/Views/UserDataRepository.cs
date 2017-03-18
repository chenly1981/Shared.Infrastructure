using Sample.Web.Entity.Views;
using Shared.Infrastructure.UnitOfWork.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Sample.Web.Entity;
using Microsoft.EntityFrameworkCore;

namespace Sample.Web.Repository.Views
{
    public class UserDataRepository : AbstractRepositoryBase<UserData>
    {
        public UserDataRepository(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {

        }

        public override List<UserData> All()
        {
            var query = CreateQueryable(null);

            return query.ToList();
        }

        public override async Task<List<UserData>> AllAsync()
        {
            var query = CreateQueryable(null);

            return await query.ToListAsync();
        }

        public override UserData Get(Expression<Func<UserData, bool>> predicate)
        {
            var query = CreateQueryable(predicate);

            return query.SingleOrDefault();
        }

        public override async Task<UserData> GetAsync(Expression<Func<UserData, bool>> predicate)
        {
            var query = CreateQueryable(predicate);

            return await query.SingleOrDefaultAsync();
        }

        public override List<UserData> Query(Expression<Func<UserData, bool>> predicate)
        {
            var query = this.CreateQueryable(predicate);

            return query.ToList();
        }

        public override async Task<List<UserData>> QueryAsync(Expression<Func<UserData, bool>> predicate)
        {
            var query = this.CreateQueryable(predicate);

            return await query.ToListAsync();
        }

        private IQueryable<UserData> CreateQueryable(Expression<Func<UserData, bool>> predicate)
        {
            var query = from user in this.Context.Set<User>()
                        join role in this.Context.Set<Role>() on user.RoleID equals role.ID
                        select new UserData
                        {
                            ID = user.ID,
                            Nickname = user.Nickname,
                            RoleID = role.ID,
                            RoleName = role.Name,
                            Username = user.Username
                        };

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }
    }
}
