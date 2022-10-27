using CommonRepository;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using Courses.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using ServicesContracts.Courses.Responses;
using ZstdSharp.Unsafe;

namespace Courses.Infrastructure.Repositories;

public class CourseWishedRepository : BaseRepository<CourseWishedDbModel, CoursesDbContext>, ICourseWishedRepository
{
    public CourseWishedRepository(CoursesDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<CourseDbModel>> GetWishedCourses(int userId)
    {
        var query = from course in Context.Courses
            join w in Context.CourseWisheds on course.Id equals w.CourseId
            where w.UserId == userId
            select course;
        return await query.ToListAsync();
       
    }
}
