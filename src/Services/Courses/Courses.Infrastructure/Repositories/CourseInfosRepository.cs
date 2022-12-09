using CommonRepository;
using CommonRepository.Models;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using Courses.Infrastructure.Extensions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Courses.Infrastructure.Repositories;

public class CourseInfosRepository : MongoBaseRepository<CourseInfoDbModel>, ICourseInfoRepository
{
    public CourseInfosRepository(IOptions<InvictusProjectDatabaseSettings> databaseSettings) : base(databaseSettings)
    {
    }

    public async Task<List<CourseInfoDbModel>> GetCoursesByModulesId(List<int> modulesIds, CancellationToken cancellationToken)
    {
        return await BaseCollection.Find(m => m.ModulesId.ContainsOnOf(modulesIds)).ToListAsync(cancellationToken);
    }
}
