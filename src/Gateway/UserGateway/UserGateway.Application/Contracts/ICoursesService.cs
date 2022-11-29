using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Requests.Modules.Queries;
using ServicesContracts.Courses.Responses;
using UserGateway.Application.Features.Courses.Queries.GetShortCourseInfo;

namespace UserGateway.Application.Contracts;

public interface ICoursesService:IUseExtendedHttpClient<ICoursesService>
{
    Task<DefaultResponseObject<CoursesVm>?> GetCoursesAsync(GetCoursesQuery query, CancellationToken cancellationToken);

    Task<DefaultResponseObject<List<ShortModuleInfoVm>>> GetShortModulesInfoByCourseId(GetShortCourseInfoQuery query,
        CancellationToken cancellationToken);

    Task<DefaultResponseObject<List<ModuleInfoVm>>> GetModulesInfoByCourseId(GetFullByCourseIdQuery query,
        CancellationToken cancellationToken);
    
    Task<DefaultResponseObject<CourseByIdVm>> GetCourseById (GetCourseByIdQuery query,
        CancellationToken cancellationToken);
}
