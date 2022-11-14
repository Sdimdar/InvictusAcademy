using CommonRepository.Models;

namespace CommonRepository.Abstractions;

public interface IMongoBaseRepository<T> where T : MongoBaseRepositoryEntity
{
    public Task<List<T>> GetAsync(CancellationToken cancellationToken);

    public Task<T?> GetAsync(int id, CancellationToken cancellationToken);

    public Task<T> CreateAsync(T entity, CancellationToken cancellationToken);

    public Task<T> UpdateAsync(int id, T entity, CancellationToken cancellationToken);

    public Task RemoveAsync(int id, CancellationToken cancellationToken);
    
}
