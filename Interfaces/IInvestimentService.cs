namespace RedisImMemmoryDB.Interfaces;

public interface IInvestimentService
{
    Task AddInvestimentAsync(Investiment investiment);
    Task<List<Investiment>> GetInvestimentsAsync(Guid userId);
}
