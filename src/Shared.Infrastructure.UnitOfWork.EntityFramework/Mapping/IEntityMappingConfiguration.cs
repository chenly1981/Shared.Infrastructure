using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.EntityFramework.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEntityMappingConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        void Map(ModelBuilder builder);
    }
}
