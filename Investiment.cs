namespace RedisImMemmoryDB;

public class Investiment
{
    public Guid Id { get; private set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime PurchaseDate { get; set; }

    public Investiment()
    {
        Id = Guid.NewGuid();
    }
}
