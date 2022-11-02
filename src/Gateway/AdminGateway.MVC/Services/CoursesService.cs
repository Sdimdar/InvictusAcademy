using AdminGateway.MVC.HttpClientExtensions;
using AdminGateway.MVC.Services.Interfaces;
using Courses.Domain.Entities;
using DataTransferLib.Models;
using ServicesContracts.Courses.Requests.Commands;
using ServicesContracts.Courses.Requests.Querries;
using ServicesContracts.Courses.Responses;

namespace AdminGateway.MVC.Services;

public class CoursesService : ICoursesService
{
    private readonly HttpClient _httpClient;

    public CoursesService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    
    public async Task<DefaultResponseObject<CourseInfoDbModel>> Create(CreateCourseCommand request)
    {
        var response = await _httpClient.PostAsJsonAsync($"/Course/Create", request);
        return await response.ReadContentAs<DefaultResponseObject<CourseInfoDbModel>>();
    }

    public async Task<DefaultResponseObject<CourseInfoDbModel>> EditCourse(EditCourseCommand request)
    {
        var response = await _httpClient.PostAsJsonAsync($"/Course/Edit", request);
        return await response.ReadContentAs<DefaultResponseObject<CourseInfoDbModel>>();
    }

    public async Task<DefaultResponseObject<CoursesVm>> GetCourses(GetCoursesQuery request)
    {
        var response = await _httpClient.PostAsJsonAsync($"/Courses/GetCourses", request);
        return await response.ReadContentAs<DefaultResponseObject<CoursesVm>>();;
    }
}