using Shared.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Shared.Infrastructure.Test.Context.TestContext;

namespace Shared.Infrastructure.Test.Repository.Interface
{
    public interface ITestEntityRepository : IRepository<TestEntity>
    {

    }
}
