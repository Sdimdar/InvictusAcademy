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
    public async Task<List<CourseDbModel>> GetWishedCourses(int userId)
    {
        var query = from course in Context.Courses
            join w in Context.CourseWisheds on course.Id equals w.CourseId
            where w.UserId == userId
            select course;
        return await query.ToListAsync();
    }
    public async Task<List<CourseDbModel>>  GetStartedCourses(int userId)
    {
        var query = from course in Context.Courses
            join p in Context.CoursePurchaseds on course.Id equals p.CourseId
            where p.UserId == userId && !p.IsCompleted
            select course;
        return await query.ToListAsync();
        
    }

    public async Task<List<CourseDbModel>> GetCompletedCourses(int userId)
    {
        var query = from course in Context.Courses
            join p in Context.CoursePurchaseds on course.Id equals p.CourseId
            where p.UserId == userId && p.IsCompleted
            select course;
        return await query.ToListAsync();
    }
}
