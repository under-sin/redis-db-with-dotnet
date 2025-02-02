using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using RedisImMemmoryDB.Entities;
using RedisImMemmoryDB.Repositories;

namespace RedisImMemmoryDB.Services;

public class InvestimentService : IInvestimentService
{
    private readonly IInvestimentRepository _investimentRepository;
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _expiration = TimeSpan.FromMinutes(5);

    public InvestimentService(IDistributedCache cache, IInvestimentRepository investimentRepository)
    {
        _cache = cache;
        _investimentRepository = investimentRepository;
    }


    public async Task<Guid> AddAsync(Investiment investiment)
    {
        await _investimentRepository.AddAsync(investiment);
        return investiment.Id;
    }

    public async Task<Investiment?> GetByIdAsync(Guid userId)
    {
        var key = $"userId_{userId}";
        
        // recupera os dados e verifica de existem
        var cachedData = await _cache.GetStringAsync(key);
        if (cachedData != null)
        {
            Console.WriteLine($"Retrieved investiment {cachedData}");
            return JsonSerializer.Deserialize<Investiment>(cachedData);
        }
        
        // busca o investimento no banco de dados e depois salva no cache
        var investiment = await _investimentRepository.GetByIdAsync(userId);
        if (investiment is null) throw new Exception("Investiment not found");
        
        await _cache.SetStringAsync(
            key, 
            JsonSerializer.Serialize(investiment),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _expiration
                // SlidingExpiration = TimeSpan.FromMinutes(2) // expira toda vez que o dado eh acessado
            }
        );
        
        return investiment;
    }
    
    public async Task<List<Investiment>> GetAllAsync()
    {
        return await _investimentRepository.GetAllAsync();
    }
    
    public async Task UpdateAsync(Investiment investiment)
    {
        await _investimentRepository.UpdateAsync(investiment);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _investimentRepository.DeleteAsync(id);
    }
}
