using CommonRepository;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using Courses.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure.Repositories;

public class CoursePurchasedRepository : BaseRepository<CoursePurchasedDbModel, CoursesDbContext>, ICoursePurchasedRepository
{
    public CoursePurchasedRepository(CoursesDbContext dbContext) : base(dbContext)
    {
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
