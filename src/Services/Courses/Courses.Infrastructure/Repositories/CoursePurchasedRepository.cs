using CommonRepository;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using Courses.Infrastructure.Persistance;

namespace Courses.Infrastructure.Repositories;

public class CoursePurchasedRepository : BaseRepository<CoursePurchasedDbModel, CoursesDbContext>, ICoursePurchasedRepository
{
    public CoursePurchasedRepository(CoursesDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<CoursePurchasedDbModel> FilterByString(IQueryable<CoursePurchasedDbModel> query, string? filterString)
    {
        return query;
    }
}
