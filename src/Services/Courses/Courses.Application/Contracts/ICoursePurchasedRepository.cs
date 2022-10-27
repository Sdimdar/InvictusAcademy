using CommonRepository.Abstractions;
using Courses.Domain.Entities;

namespace Courses.Application.Contracts;

public interface ICoursePurchasedRepository : IBaseRepository<CoursePurchasedDbModel>
{
    Task<List<CourseDbModel>> GetStartedCourses(int userId);
    Task<List<CourseDbModel>> GetCompletedCourses(int userId);
}
