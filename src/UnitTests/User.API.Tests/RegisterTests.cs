using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;

namespace User.API.Tests;

public class RegisterTests : IClassFixture<CustomApplicationFactory<Program>>
{
    private readonly ExtendedHttpClientForTests _httpClient;

    public RegisterTests(CustomApplicationFactory<Program> factory)
    {
        _httpClient = new ExtendedHttpClientForTests(factory.CreateClient());
    }

    [Fact]
    public async Task Register_SendRequestWithCorrectData()
    {
        // Arrange
        RegisterCommand command = new()
        {
            Email = "new_test@mail.ru",
            Password = "123_QWEasd",
            PasswordConfirm = "123_QWEasd",
            FirstName = "Famine",
            MiddleName = "Famine",
            LastName = "Famine",
            InstagramLink = null,
            City = "Казахстан",
            PhoneNumber = "89233047662"
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<RegisterCommand, RegisterVm>(command, "/User/Register");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
        data.Value.Should().NotBeNull();
        data.Value!.Email.Should().Be(command.Email);
    }

    [Fact]
    public async Task Register_SendRequestWithInvalidEmail()
    {
        // Arrange
        RegisterCommand command = new()
        {
            Email = "test@mail.ru",
            Password = "123_QWEasd",
            PasswordConfirm = "123_QWEasd",
            FirstName = "Famine",
            MiddleName = "Famine",
            LastName = "Famine",
            InstagramLink = null,
            City = "Казахстан",
            PhoneNumber = "89233047662"
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<RegisterCommand, RegisterVm>(command, "/User/Register");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.Errors.Should().NotBeNull();
    }

    public static IEnumerable<object[]> InvalidPasswords()
    {
        yield return new object[] { "12341231", "12341231" };
        yield return new object[] { "sdfasdvae", "sdfasdvae" };
        yield return new object[] { "123_QWEasd", "123qweasd" };
        yield return new object[] { "a1_A", "a1_A" };
        yield return new object[] { "", "" };
    }

    [Theory]
    [MemberData(nameof(InvalidPasswords))]
    public async Task Register_SendRequestWithInvalidPasswords(string password, string passwordConfirm)
    {
        // Arrange
        RegisterCommand command = new()
        {
            Email = "new_test1@mail.ru",
            Password = password,
            PasswordConfirm = passwordConfirm,
            FirstName = "Famine",
            MiddleName = "Famine",
            LastName = "Famine",
            InstagramLink = null,
            City = "Казахстан",
            PhoneNumber = "89233047662"
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<RegisterCommand, RegisterVm>(command, "/User/Register");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.ValidationErrors.Should().NotBeNull();
        data.ValidationErrors.Should().Contain(e => e.Identifier == "Password" || e.Identifier == "PasswordConfirm");
    }

    public static IEnumerable<object[]> InvalidPhoneNumbers()
    {
        yield return new object[] { "12931" };
        yield return new object[] { "asd" };
        yield return new object[] { "" };
    }

    [Theory]
    [MemberData(nameof(InvalidPhoneNumbers))]
    public async Task Register_SendRequestWithInvalidPhoneNumber(string invalidNumber)
    {
        // Arrange
        RegisterCommand command = new()
        {
            Email = "new_test1@mail.ru",
            Password = "123_QWEasd",
            PasswordConfirm = "123_QWEasd",
            FirstName = "Famine",
            MiddleName = "Famine",
            LastName = "Famine",
            InstagramLink = null,
            City = "Казахстан",
            PhoneNumber = invalidNumber
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<RegisterCommand, RegisterVm>(command, "/User/Register");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.ValidationErrors.Should().NotBeNull();
        data.ValidationErrors.Should().Contain(e => e.Identifier == "PhoneNumber");
    }

    public static IEnumerable<object[]> InvalidFirstNames()
    {
        yield return new object[] { "" };
        yield return new object[] { new string('s', 300) };
    }

    [Theory]
    [MemberData(nameof(InvalidFirstNames))]
    public async Task Register_SendRequestWithInvalidFirstName(string invalidFirstName)
    {
        // Arrange
        RegisterCommand command = new()
        {
            Email = "new_test1@mail.ru",
            Password = "123_QWEasd",
            PasswordConfirm = "123_QWEasd",
            FirstName = invalidFirstName,
            MiddleName = "Famine",
            LastName = "Famine",
            InstagramLink = null,
            City = "Казахстан",
            PhoneNumber = "89233047662"
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<RegisterCommand, RegisterVm>(command, "/User/Register");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.ValidationErrors.Should().NotBeNull();
        data.ValidationErrors.Should().Contain(e => e.Identifier == "FirstName");
    }

    public static IEnumerable<object[]> InvalidMiddleNames()
    {
        yield return new object[] { new string('s', 300) };
    }

    [Theory]
    [MemberData(nameof(InvalidMiddleNames))]
    public async Task Register_SendRequestWithInvalidMiddleName(string invalidMiddleName)
    {
        // Arrange
        RegisterCommand command = new()
        {
            Email = "new_test1@mail.ru",
            Password = "123_QWEasd",
            PasswordConfirm = "123_QWEasd",
            FirstName = "Famine",
            MiddleName = invalidMiddleName,
            LastName = "Famine",
            InstagramLink = null,
            City = "Казахстан",
            PhoneNumber = "89233047662"
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<RegisterCommand, RegisterVm>(command, "/User/Register");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.ValidationErrors.Should().NotBeNull();
        data.ValidationErrors.Should().Contain(e => e.Identifier == "MiddleName");
    }

    public static IEnumerable<object[]> InvalidLastNames()
    {
        yield return new object[] { "" };
        yield return new object[] { new string('s', 300) };
    }

    [Theory]
    [MemberData(nameof(InvalidLastNames))]
    public async Task Register_SendRequestWithInvalidLastName(string invalidLastName)
    {
        // Arrange
        RegisterCommand command = new()
        {
            Email = "new_test1@mail.ru",
            Password = "123_QWEasd",
            PasswordConfirm = "123_QWEasd",
            FirstName = "Famine",
            MiddleName = "Famine",
            LastName = invalidLastName,
            InstagramLink = null,
            City = "Казахстан",
            PhoneNumber = "89233047662"
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<RegisterCommand, RegisterVm>(command, "/User/Register");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.ValidationErrors.Should().NotBeNull();
        data.ValidationErrors.Should().Contain(e => e.Identifier == "LastName");
    }

    public static IEnumerable<object[]> InvalidInstagramLinks()
    {
        yield return new object[] { new string('s', 300) };
    }

    [Theory]
    [MemberData(nameof(InvalidInstagramLinks))]
    public async Task Register_SendRequestWithInvalidInstagramLink(string invalidInstagramLink)
    {
        // Arrange
        RegisterCommand command = new()
        {
            Email = "new_test1@mail.ru",
            Password = "123_QWEasd",
            PasswordConfirm = "123_QWEasd",
            FirstName = "Famine",
            MiddleName = "Famine",
            LastName = "Famine",
            InstagramLink = invalidInstagramLink,
            City = "Казахстан",
            PhoneNumber = "89233047662"
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<RegisterCommand, RegisterVm>(command, "/User/Register");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.ValidationErrors.Should().NotBeNull();
        data.ValidationErrors.Should().Contain(e => e.Identifier == "InstagramLink");
    }

    //public static IEnumerable<object[]> InvalidCitizenships()
    //{
    //    yield return new object[] { "" };
    //    yield return new object[] { new string('s', 300) };
    //}

    //[Theory]
    //[MemberData(nameof(InvalidCitizenships))]
    //public async Task Register_SendRequestWithInvalidCitizenship(string invalidCitizenship)
    //{
    //    // Arrange
    //    RegisterCommand command = new()
    //    {
    //        Email = "new_test1@mail.ru",
    //        Password = "123_QWEasd",
    //        PasswordConfirm = "123_QWEasd",
    //        FirstName = "Famine",
    //        MiddleName = "Famine",
    //        LastName = "Famine",
    //        InstagramLink = null,
    //        Citizenship = invalidCitizenship,
    //        PhoneNumber = "89233047662"
    //    };

    //    // Act
    //    var data = await _httpClient.PostAndReturnResponseAsync<RegisterCommand, RegisterVm>(command, "/User/Register");

    //    // Assert
    //    data.Should().NotBeNull();
    //    data.IsSuccess.Should().BeFalse();
    //    data.ValidationErrors.Should().NotBeNull();
    //    data.ValidationErrors.Should().Contain(e => e.Identifier == "Citizenship");
    //}
}
