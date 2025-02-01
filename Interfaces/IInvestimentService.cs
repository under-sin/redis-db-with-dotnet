namespace RedisImMemmoryDB.Interfaces;

public interface IInvestimentService
{
    Task<Guid> AddInvestimentAsync(Investiment investiment);
    Task<Investiment?> GetInvestimentsAsync(Guid userId);
}
