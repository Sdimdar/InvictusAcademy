using ServicesContracts.Identity.Responses;
using User.API.Tests.Fixture;

namespace User.API.Tests;

public class GetUsersDataTests : IClassFixture<CustomApplicationFactory<Program>>
{
    private const int USERS_COUNT = 4;
    private readonly IHttpClientWrapper _httpClient;
    private readonly CustomApplicationFactory<Program> _factory;
    public GetUsersDataTests(CustomApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = new HttpClientWrapper(_factory.CreateClient());
    }

    public static IEnumerable<object[]> CorrectDataWithoutFilter()
    {
        yield return new object[] { 1, 2 };
        yield return new object[] { 1, 5 };
        yield return new object[] { 2, 3 };
        yield return new object[] { 1, 100 };
    }

    [Theory]
    [MemberData(nameof(CorrectDataWithoutFilter))]
    public async Task GetUsersData_SendRequestWithCorrectDataWithoutFilter(int page, int pageSize)
    {
        // Arrange

        // Act
        var data = await _httpClient.GetAndReturnResponseAsync<UsersVm>($"/User/GetAllRegisteredUsersData?Page={page}&PageSize={pageSize}");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
        data.Value.Should().NotBeNull();
        data.Value.Users.Count().Should().Be(USERS_COUNT - pageSize * (page - 1) < pageSize ? USERS_COUNT - pageSize * (page - 1) : pageSize);
        data.Value.PageVm.PageNumber.Should().Be(page);
        data.Value.PageVm.TotalPages.Should().Be((int)Math.Ceiling(USERS_COUNT / (double)pageSize));
    }

    public static IEnumerable<object[]> CorrectDataWithFilter()
    {
        yield return new object[] { 1, 2, "Famine" };
        yield return new object[] { 2, 3, "test" };
        yield return new object[] { 1, 5, "Famine" };
        yield return new object[] { 1, 100, "test" };
    }

    [Theory]
    [MemberData(nameof(CorrectDataWithFilter))]
    public async Task GetUsersData_SendRequestWithCorrectDataWithFilter(int page, int pageSize, string filter)
    {
        // Arrange

        // Act
        var data = await _httpClient.GetAndReturnResponseAsync<UsersVm>($"/User/GetAllRegisteredUsersData?Page={page}&PageSize={pageSize}&FilterString={filter}");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
        data.Value.Should().NotBeNull();
        data.Value.Users.Count().Should().Be(USERS_COUNT - pageSize * (page - 1) < pageSize ? USERS_COUNT - pageSize * (page - 1) : pageSize);
        data.Value.PageVm.PageNumber.Should().Be(page);
        data.Value.PageVm.TotalPages.Should().Be((int)Math.Ceiling(USERS_COUNT / (double)pageSize));
    }


    [Fact]
    public async Task GetAllUsersData_SendRequestWithCorrectDataWithoutFilter()
    {
        // Arrange
        int page = 1;
        int pageSize = 0;

        // Act
        var data = await _httpClient.GetAndReturnResponseAsync<UsersVm>($"/User/GetAllRegisteredUsersData?Page={page}&PageSize={pageSize}");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
        data.Value.Should().NotBeNull();
        data.Value.Users.Count().Should().Be(USERS_COUNT);
        data.Value.PageVm.PageNumber.Should().Be(page);
        data.Value.PageVm.TotalPages.Should().Be(1);
    }

    public static IEnumerable<object[]> InvalidDataWithoutFilter()
    {
        yield return new object[] { -1, 2 };
        yield return new object[] { 0, 3 };
        yield return new object[] { 1, -100 };
    }

    [Theory]
    [MemberData(nameof(InvalidDataWithoutFilter))]
    public async Task GetUsersData_SendRequestWithWrongDataWithoutFilter(int page, int pageSize)
    {
        // Arrange

        // Act
        var data = await _httpClient.GetAndReturnResponseAsync<UsersVm>($"/User/GetAllRegisteredUsersData?Page={page}&PageSize={pageSize}");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.Value.Should().BeNull();
        data.ValidationErrors.Should().NotBeNull();
    }
}
