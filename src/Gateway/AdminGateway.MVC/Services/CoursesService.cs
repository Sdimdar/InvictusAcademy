using AdminGateway.MVC.HttpClientExtensions;
using AdminGateway.MVC.Services.Interfaces;
using CommonStructures;
using Courses.Domain.Entities;
using Courses.Domain.Entities.CourseInfo;
using DataTransferLib.Models;
using ExtendedHttpClient;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;

namespace AdminGateway.MVC.Services;

public class CoursesService : ICoursesService
{
    public ExtendedHttpClient<ICoursesService> ExtendedHttpClient { get; set; }

    public CoursesService(ExtendedHttpClient<ICoursesService> extendedHttpClient)
    {
        ExtendedHttpClient = extendedHttpClient;
    }
    
    public async Task<DefaultResponseObject<CourseVm>> Create(CreateCourseCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<CreateCourseCommand, 
        DefaultResponseObject<CourseVm>>(request,$"/Course/Create");
    }

    public async Task<DefaultResponseObject<CourseInfoDbModel>> EditCourse(EditCourseCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<EditCourseCommand, 
            DefaultResponseObject<CourseInfoDbModel>>(request, $"/Course/Edit");
    }

    public async Task<DefaultResponseObject<CoursesVm>> GetCourses(GetCoursesQuery request)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<GetCoursesQuery, 
            DefaultResponseObject<CoursesVm>>(request,$"/Courses/GetCourses");
    }

    public async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> ChangeAllModules(ChangeAllModulesCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<ChangeAllModulesCommand, 
            DefaultResponseObject<CourseInfoVm>>(request, $"/Course/ChangeAllModules");
    }

    public async Task<ActionResult<DefaultResponseObject<bool>>> Delete(DeleteCourseCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<DeleteCourseCommand, 
            DefaultResponseObject<bool>>(request, $"/Course/Delete");
    }

    public async Task<ActionResult<DefaultResponseObject<UniqueList<int>>>> GetCourseModulesId(GetCourseModulesIdQuerry request)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<UniqueList<int>>>($"/Course/GetModules?CourseId={request.CourseId}");
    }

    public async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> InsertModule(InsertModuleCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<InsertModuleCommand, 
            DefaultResponseObject<CourseInfoVm>>(request, $"/Course/InsertModule");
    }

    public async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> InsertModules(InsertModulesCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<InsertModulesCommand, 
            DefaultResponseObject<CourseInfoVm>>(request, $"/Course/InsertModules");
    }

    public async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> RemoveModule(RemoveModuleCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<RemoveModuleCommand, 
            DefaultResponseObject<CourseInfoVm>>(request, $"/Course/RemoveModule");
    }
    
    public async Task<ActionResult<DefaultResponseObject<CourseForAdminVm>>> GetCourse(GetCoursByIdQuery request)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<CourseForAdminVm>>( $"/Course/GetCourse?id={request.Id}");
    }
}