using CommonRepository;
using CommonRepository.Models;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Courses.Infrastructure.Repositories;

public class CourseInfosRepository : MongoBaseRepository<CourseInfoDbModel>, ICourseInfosRepository
{
    public CourseInfosRepository(IOptions<InvictusProjectDatabaseSettings> bookStoreDatabaseSettings) : base(bookStoreDatabaseSettings)
    {
    }
}
