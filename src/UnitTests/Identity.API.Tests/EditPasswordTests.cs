using Identity.API.Tests.Fixture;
using ServicesContracts.Identity.Requests.Commands;

namespace Identity.API.Tests;

public class EditPasswordTests : IClassFixture<CustomApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly CustomApplicationFactory<Program> _factory;
    public EditPasswordTests(CustomApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
    }

    [Fact]
    public async Task EditPassword_SendRequestWithCorrectData()
    {
        // Arrange
        EditPasswordCommand command = new()
        {
            Email = "test@mail.ru",
            OldPassword = "123_QWEasd",
            NewPassword = "123QWE_asd",
            ConfirmPassword = "123QWE_asd"
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<EditPasswordCommand, string>(command, "/User/EditPassword");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
    }

    public static IEnumerable<object[]> InvalidData()
    {
        yield return new object[] { "testi@mail.ru", "123_QWEasd", "123QWE_asd", "123QWE_asd" };
        yield return new object[] { "test@mail.ru", "123_QWEsdasd", "123QWE_asd", "123QWE_asd" };
        yield return new object[] { "test@mail.ru", "123_QWEasd", "123QWE_asd", "123QWE_assdd" };
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public async Task EditPassword_SendRequestWithInvalidData(string email, string oldPassword, string newPassword, string confirmPassword)
    {
        // Arrange
        EditPasswordCommand command = new()
        {
            Email = email,
            OldPassword = oldPassword,
            NewPassword = newPassword,
            ConfirmPassword = confirmPassword
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<EditPasswordCommand, string>(command, "/User/EditPassword");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.Errors.Should().NotBeNull();
    }

    public static IEnumerable<object[]> InvalidNewPassword()
    {
        yield return new object[] { "", "" };
        yield return new object[] { "1q@A", "1q@A" };
        yield return new object[] { "123123413", "123123413" };
        yield return new object[] { "dgsdfbsfvs", "dgsdfbsfvs" };
        yield return new object[] { "adsfsdf9as87df0a", "adsfsdf9as87df0a" };
    }

    [Theory]
    [MemberData(nameof(InvalidNewPassword))]
    public async Task EditPassword_SendRequestWithInvalidNewPassword(string newPassword, string confirmPassword)
    {
        // Arrange
        EditPasswordCommand command = new()
        {
            Email = "test@mail.ru",
            OldPassword = "123_QWEasd",
            NewPassword = newPassword,
            ConfirmPassword = confirmPassword
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<EditPasswordCommand, string>(command, "/User/EditPassword");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.ValidationErrors.Should().NotBeNull();
        data.ValidationErrors.Should().Contain(e => e.Identifier == "NewPassword");
    }
}
