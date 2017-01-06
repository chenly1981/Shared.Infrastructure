using Autofac;
using Microsoft.Extensions.Options;
using Shared.Infrastructure.Redis;
using Shared.Infrastructure.UnitOfWork;
using System;

namespace Shared.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// Register redis provider
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="redisOptions"></param>
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
        /// <param name="configAction"></param>
        /// <returns></returns>
        public static ContainerBuilder AddUnitOfWork(this ContainerBuilder builder, Action<IUnitOfWorkProvider> configAction = null)
        {
            builder.RegisterType<UnitOfWorkProvider>()
                .As<IUnitOfWorkProvider>()
                .OnActivating(e => 
                {
                    configAction?.Invoke(e.Instance);
                })    
                .SingleInstance();

            return builder;
        }
    }
}
