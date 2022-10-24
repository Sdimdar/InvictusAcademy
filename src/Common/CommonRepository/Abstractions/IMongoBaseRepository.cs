using CommonRepository.Models;

namespace CommonRepository.Abstractions;

public interface IMongoBaseRepository<T> where T : MongoBaseRepositoryEntity
{
    public Task<List<T>> GetAsync();

    public Task<T?> GetAsync(string id);

    public Task CreateAsync(T newBook);

    public Task UpdateAsync(string id, T updatedBook);

    public Task RemoveAsync(string id);
}
