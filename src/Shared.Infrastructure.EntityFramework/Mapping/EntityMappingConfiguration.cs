using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shared.Infrastructure.EntityFramework.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EntityMappingConfiguration<T> : IEntityMappingConfiguration
        where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        public abstract void Map(EntityTypeBuilder<T> b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Map(ModelBuilder builder)
        {
            builder.Entity<T>(Map);
        }
    }
}
