using CommonStructures;
using Courses.Domain.Entities.CourseInfo;
using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;

namespace AdminGateway.MVC.Services.Interfaces;

public interface ICoursesService:IUseExtendedHttpClient<ICoursesService>
{
    Task<DefaultResponseObject<CourseVm>> Create(CreateCourseCommand request);
    Task<DefaultResponseObject<CourseInfoDbModel>> EditCourse(EditCourseCommand request);
    Task<DefaultResponseObject<CoursesVm>> GetCourses(GetCoursesQuery request);
    Task<DefaultResponseObject<CourseInfoVm>> ChangeAllModules(ChangeAllModulesCommand request);
    Task<DefaultResponseObject<bool>> Delete(DeleteCourseCommand request);
    Task<DefaultResponseObject<UniqueList<int>>> GetCourseModulesId(GetCourseModulesIdQuerry request);
    Task<DefaultResponseObject<CourseInfoVm>> InsertModule(InsertModuleCommand request);
    Task<DefaultResponseObject<CourseInfoVm>> InsertModules(InsertModulesCommand request);
    Task<DefaultResponseObject<CourseInfoVm>> RemoveModule(RemoveModuleCommand request);
    Task<DefaultResponseObject<CourseForAdminVm>> GetCourse(GetCoursByIdQuery request);
}