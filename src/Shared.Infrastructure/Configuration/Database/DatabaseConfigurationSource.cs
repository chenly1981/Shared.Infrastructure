using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Configuration.Database
{
    /// <summary>
    /// Support read configuration from database
    /// </summary>
    public class DatabaseConfigurationSource : IConfigurationSource
    {
        private DatabaseConfigurationOptions Options { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public DatabaseConfigurationSource(DatabaseConfigurationOptions options)
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
        /// <param name="builder"></param>
        /// <returns></returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DatabaseConfigurationProvider(Options);
        }
    }
}
