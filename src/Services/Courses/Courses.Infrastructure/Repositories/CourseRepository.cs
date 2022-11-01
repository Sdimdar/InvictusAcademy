using CommonRepository;
using CommonRepository.Models;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using Courses.Domain.Entities.CourseInfo;
using Courses.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Courses.Infrastructure.Repositories;

public class CourseRepository : BaseRepository<CourseDbModel, CoursesDbContext>, ICourseRepository
{
    private readonly IMongoCollection<CourseInfoDbModel> _collection;

    public CourseRepository(CoursesDbContext dbContext,
                            IOptions<InvictusProjectDatabaseSettings> databaseSettings)
        : base(dbContext)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        _collection = mongoDatabase.GetCollection<CourseInfoDbModel>(databaseSettings.Value.CollectionName);
    }

    public override async Task<CourseDbModel> AddAsync(CourseDbModel entity)
    {
        var course = await base.AddAsync(entity);
        await _collection.InsertOneAsync(new CourseInfoDbModel()
        {
            Id = entity.Id,
            ModulesString = ""
        });
        return course;
    }

    public async Task<List<CourseDbModel>> GetAllActiveCourses()
    {
        IQueryable<CourseDbModel> result = Context.Courses.Where(c => c.IsActive);
        return await result.ToListAsync();
    }
    public async Task<List<CourseDbModel>> GetWishedCourses(int userId)
    {
        var query = from course in Context.Courses
                    join w in Context.CourseWisheds on course.Id equals w.CourseId
                    where w.UserId == userId
                    select course;
        return await query.ToListAsync();
    }
    public async Task<List<CourseDbModel>> GetStartedCourses(int userId)
    {
        var query = from course in Context.Courses
                    join p in Context.CoursePurchaseds on course.Id equals p.CourseId
                    where p.UserId == userId && !p.IsCompleted
                    select course;
        return await query.ToListAsync();

    }

    public async Task<List<CourseDbModel>> GetCompletedCourses(int userId)
    {
        var query = from course in Context.Courses
                    join p in Context.CoursePurchaseds on course.Id equals p.CourseId
                    where p.UserId == userId && p.IsCompleted
                    select course;
        return await query.ToListAsync();
    }
}
