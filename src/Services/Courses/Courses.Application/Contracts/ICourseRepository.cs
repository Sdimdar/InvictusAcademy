using System.Linq.Expressions;
using CommonRepository.Abstractions;
using Courses.Domain.Entities;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Contracts;

public interface ICourseRepository : IBaseRepository<CourseDbModel>
{
    Task<List<CourseDbModel>> GetAllActiveCourses();
    Task<List<CourseDbModel>> GetStartedCourses(int userId);
    Task<List<CourseDbModel>> GetCompletedCourses(int userId);
    Task<List<CourseDbModel>> GetWishedCourses(int userId);
    Task<List<CourseDbModel>> GetAllCourses();
    Task<CourseDbModel?> GetCourseById(int id);
    Task<bool> CourseIsPaid(int userId, int courseId);
    Task<List<CourseDbModel>> GetCoursesByIdList(List<int> coursesId);
    Task<List<CoursePurchasedDbModel>> GetPurchaseCourseByUserId(List<int> usersId);
    Task<List<CourseDbModel>> GetCoursesByFilter(Expression<Func<CourseDbModel, bool>> predicate);

}
