using RedisImMemmoryDB.Entities;

namespace RedisImMemmoryDB.Services;

public interface IInvestimentService
{
    Task<Guid> AddAsync(Investiment investiment);
    Task UpdateAsync(Investiment investiment);
    Task DeleteAsync(Guid id);
    Task<Investiment?> GetByIdAsync(Guid id);
    Task<List<Investiment>> GetAllAsync();
}
