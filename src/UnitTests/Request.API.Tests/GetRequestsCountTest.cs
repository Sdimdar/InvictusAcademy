namespace Request.API.Tests;

public class GetRequestsCountTest : IClassFixture<CustomApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly CustomApplicationFactory<Program> _factory;
    public GetRequestsCountTest(CustomApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
    }

    [Fact]
    public async Task GetRequestsCount_SendRequestWithCorrectData()
    {
        // Arrange

        // Act
        var data = await _httpClient.GetAndReturnResponseAsync<int>("/Request/Count");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
        data.Value.Should().Be(4);
    }

}
