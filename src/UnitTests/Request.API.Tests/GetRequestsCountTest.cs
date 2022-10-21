using FluentAssertions;
using Identity.API.Tests.Repository;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Request.Application.Contracts;
using Request.Domain.Entities;

namespace Request.API.Tests;

public class GetRequestsCountTest
{
    private readonly WebApplicationFactory<Program> _application;
    private readonly HttpClient _httpClient;

    public GetRequestsCountTest()
    {
        _application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var repositoryDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IRequestRepository));
                    services.Remove(repositoryDescriptor!);
                    services.AddSingleton<IRequestRepository, RequestMockRepository>();
                });
            });
        _httpClient = _application.CreateClient();
    }

    [Fact]
    public async Task GetRequestsCount_SendRequestWithCorrectData()
    {
        // Arrange

        var repository = _application.Services.CreateScope().ServiceProvider.GetService<IRequestRepository>();
        List<RequestDbModel> initialData = new() { new(), new() };

        if (repository is RequestMockRepository userMockRepository)
        {
            userMockRepository.InitialData(initialData);
        }

        // Act
        var response = await _httpClient.GetAsync($"Request/Count");
        response.EnsureSuccessStatusCode();
        string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var data = JsonConvert.DeserializeObject<DefaultResponseObject<int>>(dataAsString);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
        data.Value.Should().Be(initialData.Count);
    }

}
