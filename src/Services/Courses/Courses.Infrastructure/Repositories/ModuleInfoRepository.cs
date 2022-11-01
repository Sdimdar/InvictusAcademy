using CommonRepository;
using CommonRepository.Models;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ServicesContracts.Courses.Responses;
using System.Text.Json;

namespace Courses.Infrastructure.Repositories;

public class ModuleInfoRepository : MongoBaseRepository<ModuleInfoDbModel>, IModuleInfoRepository
{
    private readonly IMongoCollection<CourseInfoDbModel> _courseInfoContext;

    public ModuleInfoRepository(IOptions<InvictusProjectDatabaseSettings> databaseSettings) : base(databaseSettings)
    {
        _courseInfoContext = MongoDb.GetCollection<CourseInfoDbModel>(databaseSettings.Value.CollectionNames.GetValueOrDefault(typeof(CourseInfoDbModel)));
    }

    [Obsolete]
    public override async Task<ModuleInfoDbModel> CreateAsync(ModuleInfoDbModel entity, CancellationToken cancellationToken)
    {
        entity.Id = (await(await BaseCollection.FindAsync(_ => true, cancellationToken: cancellationToken)).ToListAsync(cancellationToken)).Last().Id + 1;
        return await base.CreateAsync(entity, cancellationToken);
    }

    public override async Task RemoveAsync(int id, CancellationToken cancellationToken)
    {
        var listOfCourses = await _courseInfoContext.Find(_ => true).ToListAsync(cancellationToken);
        var listOfCoursesThatUsedThisModule = new List<int>();
        foreach (var item in listOfCourses)
        {
            List<int> modulesIdList = new();
            if (!string.IsNullOrEmpty(item.ModulesString))
            {
                modulesIdList = item.ModulesString.Split(',')
                                                    .AsParallel()
                                                    .Select(e => int.Parse(e))
                                                    .ToList();
            }
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
        if(filterString.Length == 0)
        {
            return await GetAsync(cancellationToken);
        }
        return await (await BaseCollection.FindAsync(e => e.Title.Contains(filterString) 
                                                   || e.ShortDescription.Contains(filterString), cancellationToken: cancellationToken)).ToListAsync(cancellationToken);
    }

    public async Task<List<ModuleInfoDbModel>?> GetModulesByListOfIdAsync(IEnumerable<int> listOfId, CancellationToken cancellationToken)
    {
        return await (await BaseCollection.FindAsync(e => listOfId.Contains(e.Id), cancellationToken: cancellationToken)).ToListAsync(cancellationToken);
    }
}
