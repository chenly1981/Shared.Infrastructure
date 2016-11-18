namespace Shared.Infrastructure.Redis
{
    public class RedisOptions
    {
        public string[] EndPoints { get; }

        public string Password { get; }

        public int Db { get; }
    }
}
