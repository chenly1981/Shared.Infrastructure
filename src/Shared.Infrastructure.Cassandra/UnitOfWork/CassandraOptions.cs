using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.UnitOfWork.Cassandra
{
    public class CassandraOptions
    {
        public string[] Nodes { get; set; }

        public string KeySpace { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }
}
