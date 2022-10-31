using CommonRepository.Abstractions;
using CommonRepository.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CommonRepository;

public class MongoBaseRepository<T> : IMongoBaseRepository<T> where T : MongoBaseRepositoryEntity
{
    protected IMongoCollection<T> Collection { get; init; }
    protected IMongoDatabase MongoDb { get; init; }

    public MongoBaseRepository(IOptions<InvictusProjectDatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        MongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        Collection = MongoDb.GetCollection<T>(databaseSettings.Value.CollectionName);
    }

    public async Task<List<T>> GetAsync(CancellationToken cancellationToken) =>
        await Collection.Find(_ => true).ToListAsync(cancellationToken);

    public async Task<T?> GetAsync(int id, CancellationToken cancellationToken) =>
        await Collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

    public async Task CreateAsync(T entity, CancellationToken cancellationToken) =>
        await Collection.InsertOneAsync(entity, cancellationToken);

    public async Task UpdateAsync(int id, T entity, CancellationToken cancellationToken) =>
        await Collection.ReplaceOneAsync(x => x.Id == id, entity, cancellationToken: cancellationToken);

    public async Task RemoveAsync(int id, CancellationToken cancellationToken) =>
        await Collection.DeleteOneAsync(x => x.Id == id, cancellationToken);
}
