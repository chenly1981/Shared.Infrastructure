using Cassandra.Mapping.Attributes;
using Shared.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Test.Entity
{
    [Table("rtls_area_data_day")]
    public class RTLSAreaDataDay : IEntity
    {
        [Column("day")]
        public int Day { get; set; }
    }
}
