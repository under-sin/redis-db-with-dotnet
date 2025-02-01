using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using RedisImMemmoryDB.Interfaces;
using StackExchange.Redis;

namespace RedisImMemmoryDB;

public class InvestimentService : IInvestimentService
{
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _expiration = TimeSpan.FromMinutes(5);

    public InvestimentService(IDistributedCache cache) 
    {
        _cache = cache;
    }


    public async Task<Guid> AddInvestimentAsync(Investiment investiment)
    {
        string key = $"investiments:{investiment.Id}";

        await _cache.SetStringAsync(
            key, 
            JsonSerializer.Serialize(investiment),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = _expiration }
        );
        
        Console.WriteLine($"Added investiment {investiment.Id}");
        return investiment.Id;
    }

    public async Task<Investiment?> GetInvestimentsAsync(Guid userId)
    {
        string key = $"investiments:{userId}";
        
        var cachedData = await _cache.GetStringAsync(key);
        if (cachedData != null)
        {
            Console.WriteLine($"Retrieved investiment {cachedData}");
            return JsonSerializer.Deserialize<Investiment>(cachedData);
        }
        
        Console.WriteLine($"Item not found for investiment {userId}");
        throw new Exception($"Item not found for investiment {userId}");
    }
}
