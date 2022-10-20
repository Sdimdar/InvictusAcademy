using CommonRepository.Abstractions;
using CommonRepository.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CommonRepository;

public class MongoBaseRepository<T> : IMongoBaseRepository<T> where T : MongoBaseRepositoryEntity
{
    private readonly IMongoCollection<T> _booksCollection;

    public MongoBaseRepository(
        IOptions<InvictusProjectDatabaseSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _booksCollection = mongoDatabase.GetCollection<T>(
            bookStoreDatabaseSettings.Value.CollectionName);
    }

    public async Task<List<T>> GetAsync() =>
        await _booksCollection.Find(_ => true).ToListAsync();

    public async Task<T?> GetAsync(string id) =>
        await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(T newBook) =>
        await _booksCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, T updatedBook) =>
        await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _booksCollection.DeleteOneAsync(x => x.Id == id);
}
