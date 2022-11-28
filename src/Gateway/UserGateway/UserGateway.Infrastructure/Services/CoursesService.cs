using DataTransferLib.Models;
using ServicesContracts.Courses.Responses;
using ExtendedHttpClient;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Requests.Modules.Queries;
using UserGateway.Application.Contracts;

namespace UserGateway.Infrastructure.Services;

public class CoursesService : ICoursesService
{
    public ExtendedHttpClient<ICoursesService> ExtendedHttpClient { get; set; }
    public CoursesService(ExtendedHttpClient<ICoursesService> extendedHttpClient)
    {
        ExtendedHttpClient = extendedHttpClient;
    }
    
    public async Task<DefaultResponseObject<CoursesVm>?> GetCoursesAsync(GetCoursesQuery query, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<GetCoursesQuery, DefaultResponseObject<CoursesVm>>(query, "/Courses/GetCourses", cancellationToken);
    }

    public async Task<DefaultResponseObject<List<ShortModuleInfoVm>>> GetShortModulesInfoByCourseId(GetShortCourseInfoQuery query, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<List<ShortModuleInfoVm>>>($"/Modules/GetShortInfoByCourseId?CourseId={query.CourseId}", cancellationToken);
    }

    public async Task<DefaultResponseObject<List<ModuleInfoVm>>> GetModulesInfoByCourseId(GetFullByCourseIdQuery query, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient
            .GetAndReturnResponseAsync<DefaultResponseObject<List<ModuleInfoVm>>>($"/Modules/GetFullByCourseId?CourseId={query.CourseId}&UserId={query.UserId}", cancellationToken);
    }

    public async Task<DefaultResponseObject<PurchasedCourseInfoVm>> GetPurchasedCourseInfo(GetPurchasedCourseDataQuery query,
                                                                                           CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.
            GetAndReturnResponseAsync<DefaultResponseObject<PurchasedCourseInfoVm>>($"Courses/GetPurchasedCourseData?CourseId={query.CourseId}&UserId={query.UserId}", cancellationToken);
    }
} 
