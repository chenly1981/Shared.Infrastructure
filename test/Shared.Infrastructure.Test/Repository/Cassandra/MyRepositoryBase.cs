using Shared.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Infrastructure.Test.Repository.Cassandra
{
    public class MyRepositoryBase<T> : Shared.Infrastructure.UnitOfWork.Cassandra.RepositoryBase<T>
        where T : class, IEntity
    {
        public MyRepositoryBase(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {

        }
    }
}
