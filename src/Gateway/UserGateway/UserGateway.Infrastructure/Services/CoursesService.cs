using DataTransferLib.Models;
using ServicesContracts.Courses.Requests.Querries;
using ServicesContracts.Courses.Responses;
using DataTransferLib.Interfaces;
using UserGateway.Application.Contracts;

namespace UserGateway.Infrastructure.Services;

public class CoursesService : ICoursesService
{
    private readonly IHttpClientWrapper _httpClient;

    public CoursesService(HttpClient httpClient, IHttpClientWrapper httpClientWrapper)
    {
        _httpClient = httpClientWrapper ?? throw new ArgumentNullException(nameof(httpClientWrapper));
        _httpClient.HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    public async Task<DefaultResponseObject<CoursesVm>?> GetCoursesAsync(GetCoursesQuery query, CancellationToken cancellationToken)
    {
        return await _httpClient.GetAndReturnResponseAsync<GetCoursesQuery, CoursesVm>(query, "/Courses/GetCourses", cancellationToken);
    }
    
}
