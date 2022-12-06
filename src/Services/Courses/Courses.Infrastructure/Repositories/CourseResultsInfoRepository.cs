using CommonRepository;
using CommonRepository.Models;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using Courses.Domain.Entities.CourseResults;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Courses.Infrastructure.Repositories;

public class CourseResultsInfoRepository : MongoBaseRepository<CourseResultInfoDbModel>, ICourseResultsInfoRepository
{
    private readonly IMongoCollection<CourseResultInfoDbModel> _collection;
    public CourseResultsInfoRepository(IOptions<InvictusProjectDatabaseSettings> bookStoreDatabaseSettings,IOptions<InvictusProjectDatabaseSettings> databaseSettings) : base(bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _collection = mongoDatabase.GetCollection<CourseResultInfoDbModel>(databaseSettings.Value.CollectionNames.GetValueOrDefault(typeof(CourseResultInfoDbModel)));
    }

    public async Task<List<CourseResultInfoDbModel>> GetInfoByListId(List<int> listOfId)
    {
        List<CourseResultInfoDbModel> list = new();
        foreach (var item in listOfId)
        {
            var query = await _collection.Find(x => x.Id == item).FirstOrDefaultAsync();
            if (query is not null)
                list.Add(query);
        }
        return list;
    }
}
