using ServicesContracts.Request.Requests.Commands;

namespace Request.API.Tests;

public class ChangeCalledStatusTests : IClassFixture<CustomApplicationFactory<Program>>
{
    private readonly IHttpClientWrapper _httpClient;
    private readonly CustomApplicationFactory<Program> _factory;
    public ChangeCalledStatusTests(CustomApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = new HttpClientWrapper(_factory.CreateClient());
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
        var data = await _httpClient.PostAndReturnResponseAsync<ChangeCalledStatusCommand, string>(command, "/Request/SetCalledStatus", new CancellationToken());

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task ChangeCalledStatus_SendRequestWithWrongData()
    {
        // Arrange
        ChangeCalledStatusCommand command = new()
        {
            Id = 0
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<ChangeCalledStatusCommand, string>(command, "/Request/SetCalledStatus");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.Errors.Should().NotBeNull();
    }
}
