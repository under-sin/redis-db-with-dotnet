using MongoDB.Driver;
using RedisImMemmoryDB.Entities;

namespace RedisImMemmoryDB.Repositories;

public class InvestimentRepository : IInvestimentRepository
{
    private readonly MongoDbContext _context;

    public InvestimentRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Investiment investiment)
        => await _context.Investiments.InsertOneAsync(investiment);

    public async Task UpdateAsync(Investiment investiment)
        => await _context.Investiments.ReplaceOneAsync(i => i.Id == investiment.Id, investiment);

    public async Task DeleteAsync(Guid id)
        => await _context.Investiments.DeleteOneAsync(i => i.Id == id);

    public async Task<Investiment?> GetByIdAsync(Guid id)
        => await _context.Investiments.Find(i => i.Id == id).FirstOrDefaultAsync(); 

    public async Task<List<Investiment>> GetAllAsync()
        => await _context.Investiments.Find(_ => true).ToListAsync();
}