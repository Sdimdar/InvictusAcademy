using DataTransferLib.Models;
using ServicesContracts.Courses.Responses;
using ExtendedHttpClient;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.Courses.Requests.Courses.Querries;
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
} 
