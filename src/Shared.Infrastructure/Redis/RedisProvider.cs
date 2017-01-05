using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Net;

namespace Shared.Infrastructure.Redis
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class RedisProvider : IRedisProvider, IDisposable
    {
        private static readonly object _sync = new object();

        private ConnectionMultiplexer _pool;

        private ConnectionMultiplexer Pool
        {
            get
            {
                if (_pool == null)
                {
                    lock (_sync)
                    {
                        if (_pool == null)
                        {
                            Connect();
                        }
                    }
                }

                return _pool;
            }
        }

        private RedisOptions RedisOptions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="redisOptions"></param>
        public RedisProvider(RedisOptions redisOptions)
        {
            RedisOptions = redisOptions;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (Pool != null)
            {
                Pool.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public IDatabase GetDatabase(int? db = -1)
        {
            return Pool.GetDatabase(db ?? -1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public IServer GetServer(EndPoint endPoint = null)
        {
            if (endPoint == null)
            {
                endPoint = Pool.GetEndPoints().First();
            }

            return Pool.GetServer(endPoint);
        }

        private void Connect()
        {
            var configuration = new ConfigurationOptions
            {
                Password = RedisOptions.Password,
                DefaultDatabase = RedisOptions.Db
            };
            foreach (string endPoint in RedisOptions.EndPoints)
            {
                configuration.EndPoints.Add(EndPointCollection.TryParse(endPoint));
            }

            _pool = ConnectionMultiplexer.Connect(configuration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RedisOptions GetOptions()
        {
            return RedisOptions;
        }
    }
}
