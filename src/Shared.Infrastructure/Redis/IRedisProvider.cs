using StackExchange.Redis;

namespace Shared.Infrastructure.Redis
{
    public interface IRedisProvider
    {
        IDatabase GetDatabase(int? db = -1);

        IServer GetServer();
    }
}
