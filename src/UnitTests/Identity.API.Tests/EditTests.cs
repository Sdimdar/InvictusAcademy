using ServicesContracts.Identity.Requests.Commands;
using User.API.Tests.Fixture;

namespace User.API.Tests;

public class EditTests : IClassFixture<CustomApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly CustomApplicationFactory<Program> _factory;
    public EditTests(CustomApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
    }

    [Fact]
    public async Task Edit_SendRequestWithCorrectData()
    {
        // Arrange

        EditCommand command = new()
        {
            Email = "test@mail.ru",
            FirstName = "Famine",
            MiddleName = "Famine",
            LastName = "Famine",
            InstagramLink = null,
            Citizenship = "Казахстан",
            PhoneNumber = "82739348372"
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<EditCommand, string>(command, "/User/Edit");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Edit_SendRequestWithInvalidEmail()
    {
        // Arrange

        EditCommand command = new()
        {
            Email = "testi@mail.ru",
            FirstName = "Famine",
            MiddleName = "Famine",
            LastName = "Famine",
            InstagramLink = null,
            Citizenship = "Казахстан",
            PhoneNumber = "82739348372"
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<EditCommand, string>(command, "/User/Edit");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.Errors.Should().BeEquivalentTo(new string[] { "An error occurred while creating the request" });
    }

    public static IEnumerable<object[]> InvalidPhoneNumbers()
    {
        yield return new object[] { "12931" };
        yield return new object[] { "asd" };
        yield return new object[] { "" };
    }

    [Theory]
    [MemberData(nameof(InvalidPhoneNumbers))]
    public async Task Edit_SendRequestWithInvalidPhoneNumber(string invalidNumber)
    {
        // Arrange

        EditCommand command = new()
        {
            Email = "test@mail.ru",
            FirstName = "Famine",
            MiddleName = "Famine",
            LastName = "Famine",
            InstagramLink = null,
            Citizenship = "Казахстан",
            PhoneNumber = invalidNumber
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<EditCommand, string>(command, "/User/Edit");

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
    public async Task Edit_SendRequestWithInvalidFirstName(string invalidFirstName)
    {
        // Arrange

        EditCommand command = new()
        {
            Email = "test@mail.ru",
            FirstName = invalidFirstName,
            MiddleName = "Famine",
            LastName = "Famine",
            InstagramLink = null,
            Citizenship = "Казахстан",
            PhoneNumber = "82739348372"
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<EditCommand, string>(command, "/User/Edit");

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
    public async Task Edit_SendRequestWithInvalidMiddleName(string invalidMiddleName)
    {
        // Arrange

        EditCommand command = new()
        {
            Email = "test@mail.ru",
            FirstName = "Famine",
            MiddleName = invalidMiddleName,
            LastName = "Famine",
            InstagramLink = null,
            Citizenship = "Казахстан",
            PhoneNumber = "82739348372"
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<EditCommand, string>(command, "/User/Edit");

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
    public async Task Edit_SendRequestWithInvalidLastName(string invalidLastName)
    {
        // Arrange

        EditCommand command = new()
        {
            Email = "test@mail.ru",
            FirstName = "Famine",
            MiddleName = "Famine",
            LastName = invalidLastName,
            InstagramLink = null,
            Citizenship = "Казахстан",
            PhoneNumber = "82739348372"
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<EditCommand, string>(command, "/User/Edit");

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
    public async Task Edit_SendRequestWithInvalidInstagramLink(string invalidInstagramLink)
    {
        // Arrange

        EditCommand command = new()
        {
            Email = "test@mail.ru",
            FirstName = "Famine",
            MiddleName = "Famine",
            LastName = "Famine",
            InstagramLink = invalidInstagramLink,
            Citizenship = "Казахстан",
            PhoneNumber = "82739348372"
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<EditCommand, string>(command, "/User/Edit");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.ValidationErrors.Should().NotBeNull();
        data.ValidationErrors.Should().Contain(e => e.Identifier == "InstagramLink");
    }

    public static IEnumerable<object[]> InvalidCitizenships()
    {
        yield return new object[] { new string('s', 300) };
    }

    [Theory]
    [MemberData(nameof(InvalidCitizenships))]
    public async Task Edit_SendRequestWithInvalidCitizenship(string invalidCitizenship)
    {
        // Arrange

        EditCommand command = new()
        {
            Email = "test@mail.ru",
            FirstName = "Famine",
            MiddleName = "Famine",
            LastName = "Famine",
            InstagramLink = "",
            Citizenship = invalidCitizenship,
            PhoneNumber = "82739348372"
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<EditCommand, string>(command, "/User/Edit");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.ValidationErrors.Should().NotBeNull();
        data.ValidationErrors.Should().Contain(e => e.Identifier == "Citizenship");
    }
}
