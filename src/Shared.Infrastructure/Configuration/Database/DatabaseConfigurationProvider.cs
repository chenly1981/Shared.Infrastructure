using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Shared.Infrastructure.Configuration.Database
{
    /// <summary>
    /// Database configuration provider
    /// </summary>
    public class DatabaseConfigurationProvider : ConfigurationProvider
    {
        private DatabaseConfigurationOptions Options { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DatabaseConfigurationProvider(DatabaseConfigurationOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            Options = options;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Load()
        {
            Dictionary<string, string> data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            using (var conn = Options.ConnectionResolver.Invoke())
            {
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"select {Options.KeyColumn}, {Options.ValueColumn} from {Options.Table}";
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string key = reader.GetString(0);                            
                            if (!string.IsNullOrWhiteSpace(Options.Prefix))
                            {
                                key = $"{Options.Prefix}:{key}";
                            }

                            string value = reader.GetString(1);

                            data[key] = value;
                        }
                    }
                }
            }

            Data = data;
        }
    }
}
