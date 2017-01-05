using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Redis;
using Microsoft.Extensions.Options;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Shared.Infrastructure.Test
{
    [TestClass]
    public class RedisProviderUnitTest : UnitTestBase
    {
        [TestMethod]
        public void RedisOptionsShouldBindCorrect()
        {
            IServiceProvider serviceProvider = base.InitDependencyInjection(services =>
            {
                services.AddOptions();

                ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

                Dictionary<string, string> config = new Dictionary<string, string>();
                config.Add("Redis:EndPoints:0", "localhost:6379");
                config.Add("Redis:EndPoints:1", "localhost:6380");
                config.Add("Redis:Password", "111111");

                configurationBuilder.AddInMemoryCollection(config);

                IConfigurationRoot configuration = configurationBuilder.Build();

                services.Configure<RedisOptions>(configuration.GetSection("Redis").Bind);
            });

            var optionAccessor = serviceProvider.GetService<IOptions<RedisOptions>>();

            Assert.AreEqual(optionAccessor.Value.EndPoints.Length, 2);
            Assert.AreEqual(optionAccessor.Value.EndPoints[0], "localhost:6379");
        }

        [TestMethod]
        public void RedisProviderCanGetDatabase()
        {
            IServiceProvider serviceProvider = InitDependencyInjection(services =>
            {

            }, containerBuilder =>
            {
                containerBuilder.AddRedis(new RedisOptions
                {
                    EndPoints = new string[] { "localhost:6379" }
                });
            });

            IRedisProvider redisProvider = serviceProvider.GetService<IRedisProvider>();
            IDatabase redis = redisProvider.GetDatabase();

            Assert.IsNotNull(redis);

            IServer server = redisProvider.GetServer();
            Assert.IsNotNull(server);
        }

        [TestMethod]
        public void GetOptionsShouldWork()
        {
            IServiceProvider serviceProvider = InitDependencyInjection(services =>
            {

            }, containerBulder =>
            {
                containerBulder.AddRedis(new RedisOptions
                {
                    EndPoints = new string[] { "localhost:6379" },
                    Db = 1
                });
            });

            IRedisProvider redisProvider = serviceProvider.GetService<IRedisProvider>();
            RedisOptions redisOptions = redisProvider.GetOptions();

            Assert.IsNotNull(redisOptions);
            Assert.AreEqual(redisOptions.EndPoints.Length, 1);
            Assert.AreEqual(redisOptions.EndPoints[0], "localhost:6379");
            Assert.AreEqual(redisOptions.Db, 1);
        }
    }
}
