using Ardalis.Result;
using CommonRepository.Abstractions;
using CommonStructures;
using Courses.Domain.Entities.CourseInfo;

namespace Courses.Application.Contracts;

public interface IModuleInfoRepository : IMongoBaseRepository<ModuleInfoDbModel>
{
    Task<List<ModuleInfoDbModel>?> GetModulesByListOfIdAsync(UnicueList<int> listOfId, CancellationToken cancellationToken);
    Task<List<ModuleInfoDbModel>?> GetModulesByFilterStringAsync(string filterString, CancellationToken cancellationToken);
    Task<UnicueList<int>> CheckModulesOnExist(UnicueList<int> listOfId, CancellationToken cancellationToken);
    Task<int> GetCountAsync();
}
