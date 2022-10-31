using CommonRepository.Models;

namespace CommonRepository.Abstractions;

public interface IMongoBaseRepository<T> where T : MongoBaseRepositoryEntity
{
    public Task<List<T>> GetAsync();

    public Task<T?> GetAsync(int id);

    public Task CreateAsync(T entity);

    public Task UpdateAsync(int id, T entity);

    public Task RemoveAsync(int id);
}
