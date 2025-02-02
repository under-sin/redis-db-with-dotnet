using RedisImMemmoryDB.Entities;

namespace RedisImMemmoryDB.Repositories;

public interface IInvestimentRepository
{
    Task AddAsync(Investiment investiment);
    Task UpdateAsync(Investiment investiment);
    Task DeleteAsync(Guid id);
    Task<Investiment?> GetByIdAsync(Guid id);
    Task<List<Investiment>> GetAllAsync();
}