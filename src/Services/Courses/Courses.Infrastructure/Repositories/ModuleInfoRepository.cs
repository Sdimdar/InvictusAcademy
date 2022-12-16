using Ardalis.Result;
using CommonRepository;
using CommonRepository.Models;
using CommonStructures;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;

namespace Courses.Infrastructure.Repositories;

public class ModuleInfoRepository : MongoBaseRepository<ModuleInfoDbModel>, IModuleInfoRepository
{
    private readonly IMongoCollection<CourseInfoDbModel> _courseInfoContext;

    public ModuleInfoRepository(IOptions<InvictusProjectDatabaseSettings> databaseSettings) : base(databaseSettings)
    {
        _courseInfoContext = MongoDb.GetCollection<CourseInfoDbModel>(databaseSettings.Value.CollectionNames.GetValueOrDefault(typeof(CourseInfoDbModel)));
    }

    public override async Task RemoveAsync(int id, CancellationToken cancellationToken)
    {
        var listOfCourses = await _courseInfoContext.Find(_ => true).ToListAsync(cancellationToken);
        var listOfCoursesThatUsedThisModule = new List<int>();
        foreach (var item in listOfCourses)
        {
            List<int> modulesIdList = item.ModulesId;
            if (modulesIdList.Contains(id))
            {
                listOfCoursesThatUsedThisModule.Add(item.Id);
            }
        }
        if (listOfCoursesThatUsedThisModule.Count != 0)
            throw new InvalidOperationException($"This module is used by this courses: {JsonSerializer.Serialize(listOfCoursesThatUsedThisModule)}");
        await base.RemoveAsync(id, cancellationToken);
    }

    public async Task<List<ModuleInfoDbModel>?> GetModulesByFilterStringAsync(string filterString, CancellationToken cancellationToken)
    {
        if (filterString.Length == 0)
        {
            return await GetAsync(cancellationToken);
        }
        return await (await BaseCollection.FindAsync(e => e.Title.Contains(filterString)
                                                       || e.ShortDescription.Contains(filterString), cancellationToken: cancellationToken)).ToListAsync(cancellationToken);
    }

    public async Task<List<ModuleInfoDbModel>?> GetModulesByListOfIdAsync(UniqueList<int> listOfId, CancellationToken cancellationToken)
    {
        return await (await BaseCollection.FindAsync(e => listOfId.Contains(e.Id), cancellationToken: cancellationToken)).ToListAsync(cancellationToken);
    }

    public async Task<UniqueList<int>> CheckModulesOnExist(UniqueList<int> listOfId, CancellationToken cancellationToken)
    {
        UniqueList<int> result = (UniqueList<int>)listOfId.Clone();
        foreach (var moduleId in listOfId)
        {
            var module = await (await BaseCollection.FindAsync(e => e.Id == moduleId, cancellationToken: cancellationToken)).FirstOrDefaultAsync(cancellationToken);
            if (module is null)
                result.Remove(moduleId);
        }
        return result;
    }

    public async Task<Result<int>> GetCountAsync()
    {
        return BaseCollection.AsQueryable().ToArray().Length;
    }
}
