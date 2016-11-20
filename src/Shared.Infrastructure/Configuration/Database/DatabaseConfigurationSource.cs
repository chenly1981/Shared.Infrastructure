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
        public DatabaseConfigurationSource()
        {

        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            

            throw new NotImplementedException();
        }
    }
}
