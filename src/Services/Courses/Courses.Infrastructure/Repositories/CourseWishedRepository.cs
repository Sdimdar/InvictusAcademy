using CommonRepository;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using Courses.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure.Repositories;

public class CourseWishedRepository : BaseRepository<CourseWishedDbModel, CoursesDbContext>, ICourseWishedRepository
{
    public CourseWishedRepository(CoursesDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<CourseWishedDbModel>> GetWishedCourses(int userId)
    {
        IQueryable<CourseWishedDbModel> result = Context.CourseWisheds.Where(c => c.UserId == userId);
        return await result.ToListAsync();
    }
}
