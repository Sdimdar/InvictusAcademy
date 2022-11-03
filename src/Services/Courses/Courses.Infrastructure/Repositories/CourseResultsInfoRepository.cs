using CommonRepository;
using CommonRepository.Models;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseResults;
using Microsoft.Extensions.Options;

namespace Courses.Infrastructure.Repositories;

public class CourseResultsInfoRepository : MongoBaseRepository<CourseResultInfoDbModel>, ICourseResultsInfoRepository
{
    public CourseResultsInfoRepository(IOptions<InvictusProjectDatabaseSettings> bookStoreDatabaseSettings) : base(bookStoreDatabaseSettings)
    {
    }
}
