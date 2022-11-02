using CommonRepository.Abstractions;
using CommonRepository.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CommonRepository;

public abstract class MongoBaseRepository<T> : IMongoBaseRepository<T> where T : MongoBaseRepositoryEntity
{
    protected IMongoCollection<T> BaseCollection { get; init; }
    protected IMongoDatabase MongoDb { get; init; }

    public MongoBaseRepository(IOptions<InvictusProjectDatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        MongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        BaseCollection = MongoDb.GetCollection<T>(databaseSettings.Value.CollectionNames.GetValueOrDefault(typeof(T)));
    }

    public virtual async Task<List<T>> GetAsync(CancellationToken cancellationToken) =>
        await BaseCollection.Find(_ => true).ToListAsync(cancellationToken);

    public virtual async Task<T?> GetAsync(int id, CancellationToken cancellationToken) =>
        await BaseCollection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

    [Obsolete]
    public virtual async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await BaseCollection.InsertOneAsync(entity, cancellationToken);
        return entity;
    }

    public virtual async Task<T> UpdateAsync(int id, T entity, CancellationToken cancellationToken)
    {
        await BaseCollection.ReplaceOneAsync(x => x.Id == id, entity, cancellationToken: cancellationToken);
        return entity;
    }

    public virtual async Task RemoveAsync(int id, CancellationToken cancellationToken) =>
        await BaseCollection.DeleteOneAsync(x => x.Id == id, cancellationToken);
}
