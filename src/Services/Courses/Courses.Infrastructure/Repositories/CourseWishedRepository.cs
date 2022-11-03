using CommonRepository;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using Courses.Infrastructure.Persistance;

namespace Courses.Infrastructure.Repositories;

public class CourseWishedRepository : BaseRepository<CourseWishedDbModel, CoursesDbContext>, ICourseWishedRepository
{
    public CourseWishedRepository(CoursesDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<CourseWishedDbModel> FilterByString(IQueryable<CourseWishedDbModel> query, string? filterString)
    {
        return query;
    }
}
