using MongoDB.Driver;
using RedisImMemmoryDB.Entities;

namespace RedisImMemmoryDB;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var settings = configuration.GetSection("MongoDbSettings");
        var client = new MongoClient(settings["ConnectionString"]);
        _database = client.GetDatabase(settings["DatabaseName"]);
    }
    
    public IMongoCollection<Investiment> Investiments 
        => _database.GetCollection<Investiment>("Investiments");
}