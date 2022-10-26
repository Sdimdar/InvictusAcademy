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

    public async Task<List<CoursePurchasedDbModel>>  GetStartedCourses(int userId)
    {
        IQueryable<CoursePurchasedDbModel> result = Context.CoursePurchaseds
            .Where(c => c.UserId == userId && !c.IsCompleted);
        return await result.ToListAsync();
    }

    public async Task<List<CoursePurchasedDbModel>> GetCompletedCourses(int userId)
    {
        IQueryable<CoursePurchasedDbModel> result = Context.CoursePurchaseds
            .Where(c =>c.UserId == userId  && c.IsCompleted);
        return await result.ToListAsync();
    }
}
