using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using System.Reflection;
using System.Linq;

namespace Shared.Infrastructure.UnitOfWork
{
    public abstract class UnitOfWorkRegisteration : IUnitOfWorkRegisteration
    {
        public abstract string Name { get; }

        public abstract Type UnitOfWorkCreatorType { get; }

        public abstract Type DefaultRepositoryType { get; }

        public abstract Assembly EntityAssembly { get; }

        public abstract Assembly RepositoryAssembly { get; }

        public void Initialize(ContainerBuilder containerBuilder)
        {
            if (EntityAssembly != null)
            {
                if (DefaultRepositoryType == null)
                {
                    throw new ArgumentNullException(nameof(DefaultRepositoryType));
                }

                Type repositoryInterfaceType = typeof(IRepository<>);
                Type entityBaseType = typeof(IEntity);

                var entityTypeList = EntityAssembly.DefinedTypes.Where(t => entityBaseType.IsAssignableFrom(t.AsType()));
                foreach (var entityTypeInfo in entityTypeList)
                {
                    Type entityType = entityTypeInfo.AsType();

                    Type entityRepositoryType = DefaultRepositoryType.MakeGenericType(entityType);
                    Type entityRepositoryInterfaceType = repositoryInterfaceType.MakeGenericType(entityType);
                    containerBuilder.RegisterType(entityRepositoryType).As(entityRepositoryInterfaceType);
                }
            }

            if (RepositoryAssembly != null)
            {
                Type repositoryInterfaceType = typeof(IRepository<>);

                var repositoryTypeList = RepositoryAssembly.DefinedTypes.Where(t => repositoryInterfaceType.IsAssignableFrom(t.AsType()));
                foreach (var repositoryTypeInfo in repositoryTypeList)
                {
                    Type repositoryType = repositoryTypeInfo.AsType();

                    containerBuilder.RegisterType(repositoryType);
                }
            }

            ConfigureContainerBuilder(containerBuilder);
        }

        protected virtual void ConfigureContainerBuilder(ContainerBuilder containerBuilder)
        {

        }
    }
}
