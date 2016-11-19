using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Test.Context
{
    public class TestContext : DbContext
    {
        public DbSet<TestEntity> TestEntities { get; set; }

        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
            
        }

        public class TestEntity : IEntity
        {
            [Key]
            public int ID { get; set; }

            public string Name { get; set; }
        }
    }
}
