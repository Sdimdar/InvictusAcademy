using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Requests.Modules.Queries;
using ServicesContracts.Courses.Requests.Tests.Queries;
using ServicesContracts.Courses.Responses;

namespace UserGateway.Application.Contracts;

public interface ICoursesService : IUseExtendedHttpClient<ICoursesService>
{
    Task<DefaultResponseObject<CoursesVm>?> GetCoursesAsync(GetCoursesQuery query, CancellationToken cancellationToken);
    
    Task<DefaultResponseObject<CourseByIdVm>> GetCourseById (GetCourseByIdQuery query,
        CancellationToken cancellationToken);

    Task<DefaultResponseObject<List<ShortModuleInfoVm>>> GetShortModulesInfoByCourseId(GetShortCourseInfoQuery query,
                                                                                       CancellationToken cancellationToken);

    Task<DefaultResponseObject<List<ModuleInfoVm>>> GetModulesInfoByCourseId(GetFullByCourseIdQuery query,
                                                                             CancellationToken cancellationToken);

    Task<DefaultResponseObject<PurchasedCourseInfoVm>> GetPurchasedCourseInfo(GetPurchasedCourseDataQuery query,
                                                                              CancellationToken cancellationToken);

    Task<DefaultResponseObject<PurchasedArticleInfoVm>> GetPurchasedArticleInfo(GetPurchasedArticleQuery query,
                                                                                CancellationToken cancellationToken);

    Task<DefaultResponseObject<List<PurchasedTestVm>>> GetPurchasedTestInfo(GetPurchasedTestQuery request,
                                                                      CancellationToken cancellationToken);
}
