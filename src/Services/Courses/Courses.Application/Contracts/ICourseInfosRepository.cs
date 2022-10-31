using CommonRepository.Abstractions;
using Courses.Domain.Entities.CourseInfo;

namespace Courses.Application.Contracts;

public interface ICourseInfosRepository : IMongoBaseRepository<CourseInfoDbModel>
{
}
