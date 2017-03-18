using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.UnitOfWork.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Shared.Infrastructure.Test.Context.TestContext;
using Shared.Infrastructure.Test.Context;

namespace Shared.Infrastructure.Test.Repository.EntityFramework
{
    public class TestEntityRepository : RepositoryBase<TestEntity>
    {
        public TestEntityRepository(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {

        }

        public override void Insert(TestEntity entity)
        {
            entity.Name = "override";

            base.Insert(entity);
        }
    }
}
