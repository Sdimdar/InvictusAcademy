using CommonRepository.Abstractions;
using CommonStructures;
using Courses.Domain.Entities.CourseInfo;

namespace Courses.Application.Contracts;

public interface IModuleInfoRepository : IMongoBaseRepository<ModuleInfoDbModel>
{
    Task<List<ModuleInfoDbModel>?> GetModulesByListOfIdAsync(UniqueList<int> listOfId, CancellationToken cancellationToken);
    Task<List<ModuleInfoDbModel>?> GetModulesByFilterStringAsync(string filterString, CancellationToken cancellationToken);
    Task<UniqueList<int>> CheckModulesOnExist(UniqueList<int> listOfId, CancellationToken cancellationToken); 
}
