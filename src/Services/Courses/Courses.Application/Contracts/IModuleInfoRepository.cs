using CommonRepository.Abstractions;
using Courses.Domain.Entities.CourseInfo;

namespace Courses.Application.Contracts;

public interface IModuleInfoRepository : IMongoBaseRepository<ModuleInfoDbModel>
{
    Task<List<ModuleInfoDbModel>?> GetModulesByListOfIdAsync(IEnumerable<int> listOfId, CancellationToken cancellationToken);
    Task<List<ModuleInfoDbModel>?> GetModulesByFilterStringAsync(string filterString, CancellationToken cancellationToken);
}
