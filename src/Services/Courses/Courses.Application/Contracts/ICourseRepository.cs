using CommonRepository.Abstractions;
using Courses.Domain.Entities;

namespace Courses.Application.Contracts;

public interface ICourseRepository : IBaseRepository<CourseDbModel>
{
    Task<List<CourseDbModel>> GetAllActiveCourses();
    Task<List<CourseDbModel>> GetStartedCourses(int userId);
    Task<List<CourseDbModel>> GetCompletedCourses(int userId);
    Task<List<CourseDbModel>> GetWishedCourses(int userId);
    Task<bool> CourseIsPaid(int userId, int courseId);

}
