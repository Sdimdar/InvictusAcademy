using FluentAssertions;
using Identity.API.Tests.Repository;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Request.Application.Contracts;
using Request.Domain.Entities;
using ServicesContracts.Request.Requests.Commands;

namespace Request.API.Tests;

public class AddCommentTests
{
    private readonly WebApplicationFactory<Program> _application;
    private readonly List<RequestDbModel> _requests;
    private readonly HttpClient _httpClient;

    public AddCommentTests()
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

        var repository = _application.Services.CreateScope().ServiceProvider.GetService<IRequestRepository>();
        if (repository is RequestMockRepository userMockRepository)
        {
            userMockRepository.InitialData(_requests);
        }

        _httpClient = _application.CreateClient();
    }

    [Fact]
    public async Task AddComment_SendRequestWithCorrectData()
    {
        // Arrange
        ManagerCommentCommand command = new()
        {
            Id = 1,
            ManagerComment = "sadfasdfas"
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/Request/AddComment", command);
        response.EnsureSuccessStatusCode();
        string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var data = JsonConvert.DeserializeObject<DefaultResponseObject<string>>(dataAsString);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task AddComment_SendRequestWithInvalidId()
    {
        // Arrange
        ManagerCommentCommand command = new()
        {
            Id = 3,
            ManagerComment = "sadfasdfas"
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/Request/AddComment", command);
        response.EnsureSuccessStatusCode();
        string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var data = JsonConvert.DeserializeObject<DefaultResponseObject<string>>(dataAsString);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.Errors.Should().NotBeNull();
    }

    [Fact]
    public async Task AddComment_SendRequestWithInvalidComment()
    {
        // Arrange
        ManagerCommentCommand command = new()
        {
            Id = 1,
            ManagerComment = new string('s', 101)
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/Request/AddComment", command);
        response.EnsureSuccessStatusCode();
        string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var data = JsonConvert.DeserializeObject<DefaultResponseObject<string>>(dataAsString);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.ValidationErrors.Should().NotBeNull();
        data.ValidationErrors.Should().Contain(e => e.Identifier == "ManagerComment");
    }
}
