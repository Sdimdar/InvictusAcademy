using FluentAssertions;
using Identity.API.Tests.Repository;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Request.Application.Contracts;
using Request.Domain.Entities;
using ServicesContracts.Request.Requests.Commands;

namespace Request.API.Tests;

public class ChangeCalledStatusTests
{
    private readonly List<RequestDbModel> _requests;
    private readonly HttpClient _httpClient;

    public ChangeCalledStatusTests()
    {
        _requests = new()
        {
            new RequestDbModel()
            {
                Id = 1,
                PhoneNumber = "82739348372",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                ManagerComment = "",
                UserName = "Famine",
                WasCalled = false
            }
        };

        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var repositoryDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IRequestRepository));
                    services.Remove(repositoryDescriptor!);
                    services.AddSingleton<IRequestRepository, RequestMockRepository>();
                });
            });

        var repository = application.Services.CreateScope().ServiceProvider.GetService<IRequestRepository>();
        if (repository is RequestMockRepository userMockRepository)
        {
            userMockRepository.InitialData(_requests);
        }

        _httpClient = application.CreateClient();
    }

    [Fact]
    public async Task ChangeCalledStatus_SendRequestWithCorrectData()
    {
        // Arrange
        ChangeCalledStatusCommand command = new()
        {
            Id = 1
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/Request/SetCalledStatus", command);
        DefaultResponceObject<string>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<string>>(dataAsString);
        }

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task ChangeCalledStatus_SendRequestWithWrongData()
    {
        // Arrange
        ChangeCalledStatusCommand command = new()
        {
            Id = 2
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/Request/SetCalledStatus", command);
        DefaultResponceObject<string>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<string>>(dataAsString);
        }

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.Errors.Should().NotBeNull();
    }
}
