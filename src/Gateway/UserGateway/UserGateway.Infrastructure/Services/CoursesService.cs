using DataTransferLib.Models;
using ServicesContracts.Courses.Requests.Querries;
using ServicesContracts.Courses.Responses;
using ExtendedHttpClient;
using ExtendedHttpClient.Interfaces;
using UserGateway.Application.Contracts;

namespace UserGateway.Infrastructure.Services;

public class CoursesService : IUseExtendedHttpClient<CoursesService> ,ICoursesService
{
    public ExtendedHttpClient<CoursesService> ExtendedHttpClient { get; set; }
    public CoursesService(ExtendedHttpClient<CoursesService> extendedHttpClient)
    {
        ExtendedHttpClient = extendedHttpClient;
    }

    public async Task<DefaultResponseObject<CoursesVm>?> GetCoursesAsync(GetCoursesQuery query, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<GetCoursesQuery, DefaultResponseObject<CoursesVm>>(query, "/Courses/GetCourses", cancellationToken);
    }
} 
