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
        /// The table name to store the configuration
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// The column name to store the key
        /// </summary>
        public string KeyColumn { get; set; }

        /// <summary>
        /// The column name to store the value
        /// </summary>
        public string ValueColumn { get; set; }

        /// <summary>
        /// The prefix append in front of the key
        /// </summary>
        public string Prefix { get; set; }
    }
}
