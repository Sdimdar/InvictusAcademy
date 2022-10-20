using CommonRepository.Models;

namespace CommonRepository.Abstractions;

public interface IMongoBaseRepository<T> where T : MongoBaseRepositoryEntity
{
    public Task<List<T>> GetAsync();

    public Task<T?> GetAsync(int id);

    public Task CreateAsync(T newBook);

    public Task UpdateAsync(int id, T updatedBook);

    public Task RemoveAsync(int id);
}
