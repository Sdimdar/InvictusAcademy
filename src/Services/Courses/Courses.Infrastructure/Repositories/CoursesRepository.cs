using CommonRepository;
using CommonRepository.Models;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Courses.Infrastructure.Repositories;

public class CoursesRepository : MongoBaseRepository<CourseDbModel>, ICoursesRepository
{
    public CoursesRepository(IOptions<InvictusProjectDatabaseSettings> bookStoreDatabaseSettings) : base(bookStoreDatabaseSettings)
    {
    }
}
