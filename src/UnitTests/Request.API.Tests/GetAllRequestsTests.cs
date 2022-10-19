using FluentAssertions;
using Identity.API.Tests.Repository;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Request.Application.Contracts;
using Request.Domain.Entities;
using ServicesContracts.Request.Requests.Querries;
using ServicesContracts.Request.Responses;

namespace Request.API.Tests;

public class GetAllRequestsTests
{
    private readonly List<RequestDbModel> _requests;
    private readonly WebApplicationFactory<Program> _application;
    private readonly HttpClient _httpClient;

    public GetAllRequestsTests()
    {
        #region Requests Init
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
            },
            new RequestDbModel()
            {
                Id = 2,
                PhoneNumber = "89348473402",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                ManagerComment = "",
                UserName = "Famine",
                WasCalled = false
            },
            new RequestDbModel()
            {
                Id = 3,
                PhoneNumber = "82739348372",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                ManagerComment = "",
                UserName = "Famine",
                WasCalled = false
            },
            new RequestDbModel()
            {
                Id = 4,
                PhoneNumber = "82739348372",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                ManagerComment = "",
                UserName = "Famine",
                WasCalled = false
            }
        };
        #endregion

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

    public static IEnumerable<object[]> CorrectPagesData()
    {
        yield return new object[] { 1, 100 };
        yield return new object[] { 1, 4 };
        yield return new object[] { 1, 2 };
        yield return new object[] { 2, 2 };
        yield return new object[] { 1, 1 };
        yield return new object[] { 1, int.MaxValue };
    }

    [Theory]
    [MemberData(nameof(CorrectPagesData))]
    public async Task GetAllRequests_SendRequestWithCorrectData(int pageNumber, int pageSize)
    {
        // Arrange
        GetAllRequestCommand command = new()
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        // Act
        var response = await _httpClient.GetAsync($"/Request/GetAll?pageNumber={command.PageNumber}&pageSize={command.PageSize}");
        response.EnsureSuccessStatusCode();
        string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var data = JsonConvert.DeserializeObject<DefaultResponceObject<GetAllRequestVm>>(dataAsString);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
        data.Value.Should().NotBeNull();
        data.Value.PageSize.Should().Be(pageSize);
        data.Value.PageNumber.Should().Be(pageNumber);
        data.Value.Requests.Count.Should().Be(_requests.Count - pageSize * (pageNumber - 1) < pageSize ? _requests.Count - pageSize * (pageNumber - 1) : pageSize);
    }

    [Fact]
    public async Task GetAllRequests_SendRequestWithGetAll()
    {
        // Arrange
        GetAllRequestCommand command = new()
        {
            PageNumber = 0,
            PageSize = 0
        };

        // Act
        var response = await _httpClient.GetAsync($"/Request/GetAll?pageNumber={command.PageNumber}&pageSize={command.PageSize}");
        response.EnsureSuccessStatusCode();
        string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var data = JsonConvert.DeserializeObject<DefaultResponceObject<GetAllRequestVm>>(dataAsString);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
        data.Value.Should().NotBeNull();
        data.Value.PageSize.Should().Be(_requests.Count);
        data.Value.PageNumber.Should().Be(command.PageNumber);
        data.Value.Requests.Count.Should().Be(_requests.Count);
    }

    public static IEnumerable<object[]> InvalidPagesData()
    {
        yield return new object[] { -1, 100 };
        yield return new object[] { 1, -4 };
        yield return new object[] { 3, 10 };
    }

    [Theory]
    [MemberData(nameof(InvalidPagesData))]
    public async Task GetAllRequests_SendRequestWithINvalidData(int pageNumber, int pageSize)
    {
        // Arrange
        GetAllRequestCommand command = new()
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        // Act
        var response = await _httpClient.GetAsync($"/Request/GetAll?pageNumber={command.PageNumber}&pageSize={command.PageSize}");
        response.EnsureSuccessStatusCode();
        string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var data = JsonConvert.DeserializeObject<DefaultResponceObject<GetAllRequestVm>>(dataAsString);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
        data.Errors.Should().NotBeNull();
    }

    [Fact]
    public async Task GetAllRequests_NoDataInDb()
    {
        // Arrange
        GetAllRequestCommand command = new()
        {
            PageNumber = 1,
            PageSize = 10
        };
        var repository = _application.Services.CreateScope().ServiceProvider.GetService<IRequestRepository>();
        if (repository is RequestMockRepository userMockRepository)
        {
            userMockRepository.InitialData(new());
        }

        // Act
        var response = await _httpClient.GetAsync($"/Request/GetAll?pageNumber={command.PageNumber}&pageSize={command.PageSize}");
        response.EnsureSuccessStatusCode();
        string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var data = JsonConvert.DeserializeObject<DefaultResponceObject<GetAllRequestVm>>(dataAsString);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.Errors.Should().NotBeNull();
    }
}
