using CommonRepository.Abstractions;
using Courses.Domain.Entities;

namespace Courses.Application.Contracts;

public interface ICoursePurchasedRepository : IBaseRepository<CoursePurchasedDbModel>
{
    Task<List<CoursePurchasedDbModel>> GetStartedCourses(int userId);
    Task<List<CoursePurchasedDbModel>> GetCompletedCourses(int userId);
}
