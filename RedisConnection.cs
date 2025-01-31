using StackExchange.Redis;

namespace RedisImMemmoryDB;

public class RedisConnection
{
    private static Lazy<ConnectionMultiplexer> _lazyConnection;

    static RedisConnection() 
    {
        try
        {
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() => 
            {
                return ConnectionMultiplexer.Connect("localhost:6379");
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MENSAGEM DE ERROR - {ex.Message}");
        }
    }

    public static ConnectionMultiplexer Connection = _lazyConnection.Value;
}
