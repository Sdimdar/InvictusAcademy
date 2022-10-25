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

    protected override IQueryable<CourseDbModel> FilterByString(IQueryable<CourseDbModel> query, string? filterString)
    {
        return string.IsNullOrEmpty(filterString)
            ? query
            : query.Where(v => v.Name.ToLower().Contains(filterString.ToLower())
                            || v.Description.ToLower().Contains(filterString.ToLower())
            );
    }
}
