using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Configuration.Database
{
    /// <summary>
    /// Database configuration options
    /// </summary>
    public class DatabaseConfigurationOptions
    {
        /// <summary>
        /// The resolver to get connection
        /// </summary>
        public Func<IDbConnection> ConnectionResolver { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string KeyColumn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ValueColumn { get; set; }
    }
}
