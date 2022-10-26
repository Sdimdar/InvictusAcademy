using CommonRepository;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using Courses.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure.Repositories;

public class CourseRepository : BaseRepository<CourseDbModel, CoursesDbContext>, ICourseRepository
{
    public CourseRepository(CoursesDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<CourseDbModel>> GetAllActiveCourses()
    {
        IQueryable<CourseDbModel> result = Context.Courses.Where(c => c.IsActive);
        return await result.ToListAsync();
    }
}
