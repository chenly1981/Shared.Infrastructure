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

        public abstract Assembly[] EntityAssemblies { get; }

        public abstract Assembly[] RepositoryAssemblies { get; }

        public void Initialize(ContainerBuilder containerBuilder)
        {
            if (EntityAssemblies?.Length > 0)
            {
                if (DefaultRepositoryType == null)
                {
                    throw new ArgumentNullException(nameof(DefaultRepositoryType));
                }

                Type repositoryInterfaceType = typeof(IRepository<>);
                Type entityBaseType = typeof(IEntity);

                var entityTypeList = EntityAssemblies.SelectMany(assembly =>
                    assembly.DefinedTypes.Where(t =>
                        t.IsClass && !t.IsAbstract && !t.IsGenericType && entityBaseType.IsAssignableFrom(t.AsType())
                    )
                );
                foreach (var entityTypeInfo in entityTypeList)
                {
                    Type entityType = entityTypeInfo.AsType();

                    Type entityRepositoryType = DefaultRepositoryType.MakeGenericType(entityType);
                    Type entityRepositoryInterfaceType = repositoryInterfaceType.MakeGenericType(entityType);
                    containerBuilder.RegisterType(entityRepositoryType).As(entityRepositoryInterfaceType);
                }
            }

            if (RepositoryAssemblies?.Length > 0)
            {
                Type repositoryInterfaceType = typeof(IRepository);

                var repositoryTypeList = RepositoryAssemblies.SelectMany(assembly =>
                    assembly.DefinedTypes.Where(t =>
                        t.IsClass && !t.IsAbstract && !t.IsGenericType && repositoryInterfaceType.IsAssignableFrom(t.AsType())
                    )
                );
                foreach (var repositoryTypeInfo in repositoryTypeList)
                {
                    Type repositoryType = repositoryTypeInfo.AsType();

                    containerBuilder.RegisterType(repositoryType).AsSelf().AsImplementedInterfaces();
                }
            }

            ConfigureContainerBuilder(containerBuilder);
        }

        protected virtual void ConfigureContainerBuilder(ContainerBuilder containerBuilder)
        {

        }
    }
}
