using FluentAssertions;
using ServicesContracts.Request.Requests.Commands;

namespace Request.API.Tests;

public class CreateRequestTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly WebApplicationFactory<Program> _factory;
    public CreateRequestTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
    }

    [Fact]
    public async Task CreateRequest_SendRequestWithCorrectData()
    {
        // Arrange
        CreateRequestCommand command = new()
        {
            UserName = "Famine",
            PhoneNumber = "8 (193) 743 92 93"
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/Request/Create", command);
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

    public static IEnumerable<object[]> InvalidUserName()
    {
        yield return new object[] { "" };
    }

    [Theory]
    [MemberData(nameof(InvalidUserName))]
    public async Task CreateRequest_SendRequestWithWrongUserName(string userName)
    {
        // Arrange
        CreateRequestCommand command = new()
        {
            UserName = userName,
            PhoneNumber = "8 (193) 743 92 93"
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/Request/Create", command);
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
        data.ValidationErrors.Should().NotBeNull();
        data.ValidationErrors.Should().Contain(e => e.Identifier == "UserName");
    }

    public static IEnumerable<object[]> InvalidPhoneNumber()
    {
        yield return new object[] { "8 (193) 743 123123123123192 93" };
        yield return new object[] { "dfadsf123123123192 93" };
        yield return new object[] { "#(*&FH(A" };
        yield return new object[] { "0319475983245702934" };
        yield return new object[] { "" };
    }

    [Theory]
    [MemberData(nameof(InvalidPhoneNumber))]
    public async Task CreateRequest_SendRequestWithWrongPhoneNumber(string phoneNumber)
    {
        // Arrange
        CreateRequestCommand command = new()
        {
            UserName = "Famine",
            PhoneNumber = phoneNumber
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/Request/Create", command);
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
        data.ValidationErrors.Should().NotBeNull();
        data.ValidationErrors.Should().Contain(e => e.Identifier == "PhoneNumber");
    }
}
