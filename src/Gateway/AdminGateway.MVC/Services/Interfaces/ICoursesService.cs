using CommonStructures;
using Courses.Domain.Entities;
using Courses.Domain.Entities.CourseInfo;
using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;

namespace AdminGateway.MVC.Services.Interfaces;

public interface ICoursesService:IUseExtendedHttpClient<ICoursesService>
{
    Task<DefaultResponseObject<CourseVm>> Create(CreateCourseCommand request);
    Task<DefaultResponseObject<CourseInfoDbModel>> EditCourse(EditCourseCommand request);
    Task<DefaultResponseObject<CoursesVm>> GetCourses(GetCoursesQuery request);
    Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> ChangeAllModules(ChangeAllModulesCommand request);
    Task<ActionResult<DefaultResponseObject<bool>>> Delete(DeleteCourseCommand request);
    Task<ActionResult<DefaultResponseObject<UniqueList<int>>>> GetCourseModulesId(GetCourseModulesIdQuerry request);
    Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> InsertModule(InsertModuleCommand request);
    Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> InsertModules(InsertModulesCommand request);
    Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> RemoveModule(RemoveModuleCommand request);
    Task<ActionResult<DefaultResponseObject<CourseForAdminVm>>> GetCourse(GetCoursByIdQuery request);
}