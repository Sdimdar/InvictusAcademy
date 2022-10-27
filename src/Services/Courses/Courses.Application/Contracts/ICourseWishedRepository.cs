using CommonRepository.Abstractions;
using Courses.Domain.Entities;

namespace Courses.Application.Contracts;

public interface ICourseWishedRepository : IBaseRepository<CourseWishedDbModel>
{
    Task<List<CourseDbModel>> GetWishedCourses(int userId);
}
