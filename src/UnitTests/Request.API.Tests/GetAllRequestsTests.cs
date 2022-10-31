using ServicesContracts.Request.Requests.Querries;
using ServicesContracts.Request.Responses;

namespace Request.API.Tests;

public class GetAllRequestsTests : IClassFixture<CustomApplicationFactory<Program>>
{
    private const int USERS_COUNT = 4;
    private readonly HttpClient _httpClient;
    private readonly CustomApplicationFactory<Program> _factory;
    public GetAllRequestsTests(CustomApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
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
        GetAllRequestsQuery command = new()
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        // Act
        var data = await _httpClient.GetAndReturnResponseAsync<GetAllRequestVm>($"/Request/GetAll?pageNumber={command.PageNumber}&pageSize={command.PageSize}");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
        data.Value.Should().NotBeNull();
        data.Value.PageSize.Should().Be(pageSize);
        data.Value.PageNumber.Should().Be(pageNumber);
        data.Value.Requests.Count.Should().Be(USERS_COUNT - pageSize * (pageNumber - 1) < pageSize ? USERS_COUNT - pageSize * (pageNumber - 1) : pageSize);
    }

    [Fact]
    public async Task GetAllRequests_SendRequestWithGetAll()
    {
        // Arrange
        GetAllRequestsQuery command = new()
        {
            PageNumber = 1,
            PageSize = 0
        };

        // Act
        var data = await _httpClient.GetAndReturnResponseAsync<GetAllRequestVm>($"/Request/GetAll?pageNumber={command.PageNumber}&pageSize={command.PageSize}");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
        data.Value.Should().NotBeNull();
        data.Value.PageSize.Should().Be(4);
        data.Value.PageNumber.Should().Be(command.PageNumber);
        data.Value.Requests.Count.Should().Be(USERS_COUNT);
    }

    public static IEnumerable<object[]> InvalidPagesData()
    {
        yield return new object[] { -1, 100 };
        yield return new object[] { 1, -4 };
        yield return new object[] { 0, 10 };
    }

    [Theory]
    [MemberData(nameof(InvalidPagesData))]
    public async Task GetAllRequests_SendRequestWithInvalidData(int pageNumber, int pageSize)
    {
        // Arrange
        GetAllRequestsQuery command = new()
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        // Act
        var data = await _httpClient.GetAndReturnResponseAsync<GetAllRequestVm>($"/Request/GetAll?pageNumber={command.PageNumber}&pageSize={command.PageSize}");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.Value.Should().BeNull();
        data.ValidationErrors.Should().NotBeNull();
    }
}
