using CommonRepository.Abstractions;
using Courses.Domain.Entities;

namespace Courses.Application.Contracts;

public interface ICourseRepository : IBaseRepository<CourseDbModel>
{
}
