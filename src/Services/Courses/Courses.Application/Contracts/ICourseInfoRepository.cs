using CommonRepository.Abstractions;
using Courses.Domain.Entities.CourseInfo;

namespace Courses.Application.Contracts;

public interface ICourseInfoRepository : IMongoBaseRepository<CourseInfoDbModel>
{
    Task<List<CourseInfoDbModel>> GetCoursesByModulesId(List<int> modulesIds, CancellationToken cancellationToken);
}
