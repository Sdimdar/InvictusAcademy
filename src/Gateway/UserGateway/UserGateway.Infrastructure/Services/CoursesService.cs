using DataTransferLib.Models;
using Newtonsoft.Json;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using System.Text;
using UserGateway.Application.Contracts;
using UserGateway.Application.Features.Courses.Queries.GetCourses;
using UserGateway.Infrastructure.Extensions;

namespace UserGateway.Infrastructure.Services;

public class CoursesService : ICoursesService
{
    private readonly HttpClient _httpClient;

    public CoursesService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    public async Task<DefaultResponseObject<CoursesVm>?> GetCoursesAsync(GetCoursesQuery query, CancellationToken cancellationToken)
    {
        var requestMessage = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            Content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json"),
            RequestUri = new Uri(_httpClient.BaseAddress, "/Courses/GetCourses")
        };
        var responce = await _httpClient.SendAsync(requestMessage, cancellationToken);
        return await responce.ReadContentAs<DefaultResponseObject<CoursesVm>?>();
    }
    
}
