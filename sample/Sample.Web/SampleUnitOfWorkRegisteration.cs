using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sample.Web
{
    public class SampleUnitOfWorkRegisteration : Shared.Infrastructure.EntityFramework.UnitOfWorkRegisteration<Context.SampleDbContext>
    {
        public override string Name => Consts.UnitOfWorkNames.SAMPLE;

        public override Assembly[] EntityAssemblies => new Assembly[] { Assembly.Load(new AssemblyName("Sample.Web")) };

        public override Assembly[] RepositoryAssemblies => new Assembly[] { Assembly.Load(new AssemblyName("Sample.Web")) };
    }
}
