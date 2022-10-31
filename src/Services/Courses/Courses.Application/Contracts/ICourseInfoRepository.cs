using CommonRepository.Abstractions;
using Courses.Domain.Entities.CourseInfo;

namespace Courses.Application.Contracts;

public interface ICourseInfoRepository : IMongoBaseRepository<CourseInfoDbModel>
{
    Task<CourseInfoDbModel> InsertModuleAsync(int courseId, int moduleId, int index, CancellationToken cancellationToken);
    Task<CourseInfoDbModel> RemoveModuleAsync(int courseId, int moduleId, CancellationToken cancellationToken);
    Task<CourseInfoDbModel> InsertModulesAsync(int courseId, IEnumerable<int> modulesId, int startIndex, CancellationToken cancellationToken);
    Task<CourseInfoDbModel> ChangeAllModulesAsync(int courseId, IEnumerable<int> modulesId, CancellationToken cancellationToken);
    Task<List<int>> GetModulesId(int courseId, CancellationToken cancellationToken);
}
