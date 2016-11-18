using Autofac;
using Shared.Infrastructure.Redis;
using Shared.Infrastructure.UnitOfWork;

namespace Shared.Infrastructure
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder AddRedis(this ContainerBuilder builder)
        {
            builder.RegisterType<RedisProvider>().As<IRedisProvider>().SingleInstance();

            return builder;
        }

        public static ContainerBuilder AddUnitOfWork(this ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWorkProvider>()
                .As<IUnitOfWorkProvider>()
                .SingleInstance();

            return builder;
        }
    }
}
