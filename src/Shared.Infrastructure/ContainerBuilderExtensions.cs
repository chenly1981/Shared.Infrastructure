using Autofac;
using Microsoft.Extensions.Options;
using Shared.Infrastructure.Redis;
using Shared.Infrastructure.UnitOfWork;

namespace Shared.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// Register redis provider
        /// You should configure RedisOptions before you do this by calling services.Configure<RedisOptions>()
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ContainerBuilder AddRedis(this ContainerBuilder builder, RedisOptions redisOptions)
        {
            RedisProvider redisProvider = new RedisProvider(redisOptions);
            builder.RegisterInstance(redisProvider).As<IRedisProvider>().SingleInstance();

            return builder;
        }

        /// <summary>
        /// Register UnitOfWork provider
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ContainerBuilder AddUnitOfWork(this ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWorkProvider>()
                .As<IUnitOfWorkProvider>()
                .SingleInstance();

            return builder;
        }
    }
}
