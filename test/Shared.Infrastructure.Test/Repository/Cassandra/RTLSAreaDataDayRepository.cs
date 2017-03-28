using Shared.Infrastructure.Test.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Infrastructure.Test.Repository.Cassandra
{
    public class RTLSAreaDataDayRepository : MyRepositoryBase<RTLSAreaDataDay>
    {
        public RTLSAreaDataDayRepository(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {

        }
    }
}
