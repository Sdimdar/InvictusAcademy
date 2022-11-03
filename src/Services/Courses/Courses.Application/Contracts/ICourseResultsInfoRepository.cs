using CommonRepository.Abstractions;
using Courses.Domain.Entities.CourseResults;

namespace Courses.Application.Contracts;

public interface ICourseResultsInfoRepository : IMongoBaseRepository<CourseResultInfoDbModel>
{
}
