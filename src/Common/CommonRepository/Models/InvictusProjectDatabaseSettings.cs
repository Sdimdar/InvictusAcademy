namespace CommonRepository.Models;

public class InvictusProjectDatabaseSettings
{

    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public Dictionary<Type, string> CollectionNames { get; set; } = null!;
}
