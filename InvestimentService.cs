using System.Text.Json;
using RedisImMemmoryDB.Interfaces;
using StackExchange.Redis;

namespace RedisImMemmoryDB;

public class InvestimentService : IInvestimentService
{
    private readonly IDatabase _db;

    public InvestimentService() 
    {
        _db = RedisConnection.Connection.GetDatabase();
    }

    public async Task AddInvestimentAsync(Investiment investiment) 
    {
        string key = $"investiments:{investiment.Id}";

        var investimentJson = JsonSerializer.Serialize(investiment);

        await _db.ListLeftPushAsync(key, investimentJson);
    }

    public async Task<List<Investiment>> GetInvestimentsAsync(Guid userId)
    {
        string key = $"investiments:{userId}";

        long listlength = await _db.ListLengthAsync(key);

        var range = await _db.ListRangeAsync(key, 0, listlength-1);

        // desserializa cada item da lista
        List<Investiment> investiments = [];
        foreach (var item in range) 
        {
            var investiment = JsonSerializer.Deserialize<Investiment>(item.ToString());
            if (investiment != null) 
            {
                investiments.Add(investiment);
            }
        }

        return investiments;
    }
}
