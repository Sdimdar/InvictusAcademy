using CommonRepository;
using CommonRepository.Models;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Courses.Infrastructure.Repositories;

public class CourseInfosRepository : MongoBaseRepository<CourseInfoDbModel>, ICourseInfoRepository
{
    private readonly IMongoCollection<ModuleInfoDbModel> _moduleInfoCollection;

    public CourseInfosRepository(IOptions<InvictusProjectDatabaseSettings> databaseSettings) : base(databaseSettings)
    {
        _moduleInfoCollection = MongoDb.GetCollection<ModuleInfoDbModel>(databaseSettings.Value.CollectionName);
    }

    public async Task<CourseInfoDbModel> ChangeAllModulesAsync(int courseId,
                                                               IEnumerable<int> modulesId,
                                                               CancellationToken cancellationToken)
    {
        var course = await GetAsync(courseId, cancellationToken);
        if (course is null) throw new KeyNotFoundException($"Course with this ID: {courseId} is not found");
        foreach (var moduleId in modulesId)
        {
            if ((await _moduleInfoCollection.FindAsync(e => e.Id == moduleId, cancellationToken: cancellationToken))
                                           .Any(cancellationToken: cancellationToken))
                throw new KeyNotFoundException($"Module with ID: {moduleId} is not found, abort operation");
        }
        string modulesString = string.Join(',', modulesId);
        course.CourseInfo.ModulesString = modulesString;
        await UpdateAsync(courseId, course, cancellationToken);
        return course;
    }

    public async Task<List<int>> GetModulesId(int courseId, CancellationToken cancellationToken)
    {
        var course = await GetAsync(courseId, cancellationToken);
        if (course is null) throw new KeyNotFoundException($"Course with this ID: {courseId} is not found");
        List<int> result = course.CourseInfo.ModulesString.Split(',')
                                                          .AsParallel()
                                                          .Select(e => int.Parse(e))
                                                          .ToList();
        return result;
    }

    public async Task<CourseInfoDbModel> InsertModuleAsync(int courseId,
                                                           int moduleId,
                                                           int index,
                                                           CancellationToken cancellationToken)
    {
        var course = await GetAsync(courseId, cancellationToken);
        if (course is null) throw new KeyNotFoundException($"Course with this ID: {courseId} is not found");
        if ((await _moduleInfoCollection.FindAsync(e => e.Id == moduleId, cancellationToken: cancellationToken))
                                        .Any(cancellationToken: cancellationToken))
            throw new KeyNotFoundException($"Module with ID: {moduleId} is not found, abort operation");
        List<int> modulesIdList = course.CourseInfo.ModulesString.Split(',')
                                                                 .AsParallel()
                                                                 .Select(e => int.Parse(e))
                                                                 .ToList();
        if (index < 0)
        {
            modulesIdList.Add(moduleId);
        }
        else
        {
            modulesIdList.Insert(index, moduleId);
        }
        string modulesString = string.Join(',', modulesIdList);
        course.CourseInfo.ModulesString = modulesString;
        await UpdateAsync(courseId, course, cancellationToken);
        return course;
    }

    public async Task<CourseInfoDbModel> InsertModulesAsync(int courseId,
                                                            IEnumerable<int> modulesId,
                                                            int startIndex,
                                                            CancellationToken cancellationToken)
    {
        var course = await GetAsync(courseId, cancellationToken);
        if (course is null) throw new KeyNotFoundException($"Course with this ID: {courseId} is not found");
        foreach (var moduleId in modulesId)
        {
            if ((await _moduleInfoCollection.FindAsync(e => e.Id == moduleId, cancellationToken: cancellationToken))
                                            .Any(cancellationToken: cancellationToken))
                throw new KeyNotFoundException($"Module with ID: {moduleId} is not found, abort operation");
        }
        List<int> modulesIdList = course.CourseInfo.ModulesString.Split(',')
                                                                 .AsParallel()
                                                                 .Select(e => int.Parse(e))
                                                                 .ToList();
        if (startIndex < 0)
        {
            modulesIdList.AddRange(modulesId);
        }
        else
        {
            modulesIdList.InsertRange(startIndex, modulesId);
        }
        string modulesString = string.Join(',', modulesIdList);
        course.CourseInfo.ModulesString = modulesString;
        await UpdateAsync(courseId, course, cancellationToken);
        return course;
    }

    public async Task<CourseInfoDbModel> RemoveModuleAsync(int courseId,
                                                           int moduleId,
                                                           CancellationToken cancellationToken)
    {
        var course = await GetAsync(courseId, cancellationToken);
        if (course is null) throw new KeyNotFoundException($"Course with this ID: {courseId} is not found");
        List<int> modulesIdList = course.CourseInfo.ModulesString.Split(',')
                                                                 .AsParallel()
                                                                 .Select(e => int.Parse(e))
                                                                 .ToList();
        modulesIdList.Remove(moduleId);
        string modulesString = string.Join(',', modulesIdList);
        course.CourseInfo.ModulesString = modulesString;
        await UpdateAsync(courseId, course, cancellationToken);
        return course;
    }
}
