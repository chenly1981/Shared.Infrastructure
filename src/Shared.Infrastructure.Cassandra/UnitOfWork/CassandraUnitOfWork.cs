using Autofac;
using Autofac.Core;
using Cassandra;
using Shared.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.UnitOfWork.Cassandra
{
    public class CassandraUnitOfWork : UnitOfWorkBase, IUnitOfWork
    {
        private ISession Session { get; set; }

        private IComponentContext ComponentContext { get; set; }

        public CassandraUnitOfWork(ISession session, IComponentContext componentContext)
        {
            this.Session = session;
            this.ComponentContext = componentContext;
        }

        public override ITransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        protected override IRepository<T> ResolveDefaultRepository<T>()
        {
            Parameter parameter = TypedParameter.From(this.Session);

            return this.ComponentContext.Resolve<RepositoryBase<T>>(parameter);
        }

        protected override T ResolveRepository<T>()
        {
            Parameter parameter = TypedParameter.From(this.Session);

            return this.ComponentContext.Resolve<T>(parameter);
        }

        public override void Dispose()
        {
            base.Dispose();

            if (this.Session != null)
            {
                this.Session.Dispose();
            }
        }
    }
}
