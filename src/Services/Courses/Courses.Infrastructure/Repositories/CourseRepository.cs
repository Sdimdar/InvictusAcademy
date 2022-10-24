using CommonRepository;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using Courses.Infrastructure.Persistance;

namespace Courses.Infrastructure.Repositories;

public class CourseRepository : BaseRepository<CourseDbModel, CoursesDbContext>, ICourseRepository
{
    public CourseRepository(CoursesDbContext dbContext) : base(dbContext)
    {
    }
}
