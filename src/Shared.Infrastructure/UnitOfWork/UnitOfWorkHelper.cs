using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.UnitOfWork
{
    public sealed class UnitOfWorkHelper
    {
        const string UNIT_OF_WORK_CREATOR_PREFIX = "UnitOfWorkCreator_";

        public static string GetUnitOfWorkName(string alias)
        {
            return UNIT_OF_WORK_CREATOR_PREFIX + alias;
        }
    }
}
