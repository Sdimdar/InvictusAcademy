﻿using System.Linq.Expressions;
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

        _collection = mongoDatabase.GetCollection<CourseInfoDbModel>(databaseSettings.Value.CollectionNames.GetValueOrDefault(typeof(CourseInfoDbModel)));
    }

    public override async Task<bool> DeleteAsync(CourseDbModel entity)
    {
        Context.Remove(entity);
        var result = await _collection.DeleteOneAsync(e => e.Id == entity.Id);
        if (result.IsAcknowledged)
        {
            await Context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public override async Task<CourseDbModel> AddAsync(CourseDbModel entity)
    {
        var course = await base.AddAsync(entity);
        await _collection.InsertOneAsync(new CourseInfoDbModel()
        {
            Id = entity.Id
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

    public async Task<List<CourseDbModel>> GetAllCourses()
    {
        IQueryable<CourseDbModel> result = Context.Courses;
        return await result.ToListAsync();
    }

    public async Task<CourseDbModel?> GetCourseById(int id)
    {
        var result = await Context.Courses.FirstOrDefaultAsync(c => c.Id == id);
        var points = await Context.CoursePoints.Where(c => c.CourseId == result!.Id).ToListAsync();
        result!.CoursePoints = points;
        return result;
    }

    public async Task<bool> CourseIsPaid(int userId, int courseId)
    {
        var query = await Context.CoursePurchaseds
            .FirstOrDefaultAsync(c => c.UserId == userId && c.CourseId == courseId);
        if (query is null) return false;
        return true;
    }
    
    public async Task<List<CourseDbModel>> GetCoursesByIdList(List<int> coursesId)
    {
        List<CourseDbModel> list = new();
        foreach (var item in coursesId)
        {
            var query = await Context.Courses.FirstOrDefaultAsync(c => c.Id == item);
            if (query is not null)
                list.Add(query);
        }

        return list;
    }

    public async Task<List<CourseDbModel>> GetCoursesByFilter(Expression<Func<CourseDbModel, bool>> predicate)
    {
        return await Context.Courses.Where(predicate).ToListAsync();
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

    protected override IQueryable<CourseDbModel> FilterByString(IQueryable<CourseDbModel> query, string? filterString)
    {
        return string.IsNullOrEmpty(filterString)
            ? query
            : query.Where(v => v.Name.ToLower().Contains(filterString.ToLower())
                            || v.Description.ToLower().Contains(filterString.ToLower())
            );
    }

    public async Task<List<CoursePurchasedDbModel>> GetPurchaseCourseByUserId(List<int> usersId)
    {
        List<CoursePurchasedDbModel> list = new();
        foreach (var item in usersId)
        {
            var query = await Context.CoursePurchaseds.FirstOrDefaultAsync(c => c.UserId == item);
            if (query is not null)
                list.Add(query);
        }
        return list;
    }
}
