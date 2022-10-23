using ServicesContracts.Identity.Responses;
using User.API.Tests.Fixture;

namespace User.API.Tests;

public class GetUserDataTests : IClassFixture<CustomApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly CustomApplicationFactory<Program> _factory;
    public GetUserDataTests(CustomApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
    }

    [Theory]
    [InlineData("test@test.ru")]
    public async Task GetUserData_SendRequestWithCorrectData(string email)
    {
        // Arrange

        // Act
        var data = await _httpClient.GetAndReturnResponseAsync<UserVm>($"/User/GetUserData?email={email}");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
        data.Value.Should().NotBeNull();
        data.Value.Email.Should().Be(email);
    }

    public static IEnumerable<object[]> InvalidEmails()
    {
        yield return new object[] { "testi@mail.ru" };
        yield return new object[] { "asd" };
    }

    [Theory]
    [MemberData(nameof(InvalidEmails))]
    public async Task GetUserData_SendRequestWithInvalidEmail(string invalidEmail)
    {
        // Arrange

        // Act
        var data = await _httpClient.GetAndReturnResponseAsync<UserVm>($"/User/GetUserData?email={invalidEmail}");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.Errors.Should().NotBeNull();
    }

    [Theory]
    [InlineData("")]
    public async Task GetUserData_SendRequestWithEmptyStringEmail(string invalidEmail)
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync($"/User/GetUserData?email={invalidEmail}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
