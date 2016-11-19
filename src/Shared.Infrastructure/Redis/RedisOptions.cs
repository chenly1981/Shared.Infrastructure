namespace Shared.Infrastructure.Redis
{
    public class RedisOptions
    {
        /// <summary>
        /// Redis end points, such as "{host or ip}:{port}"
        /// </summary>
        public string[] EndPoints { get; }

        /// <summary>
        /// Redis password
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// Default redis database
        /// </summary>
        public int Db { get; }
    }
}
