namespace User.API.Tests;

public class GetUsersCountTests : IClassFixture<CustomApplicationFactory<Program>>
{
    private readonly ExtendedHttpClientForTests _httpClient;

    public GetUsersCountTests(CustomApplicationFactory<Program> factory)
    {
        _httpClient = new ExtendedHttpClientForTests(factory.CreateClient());
    }

    [Fact]
    public async Task GetRequestsCount_SendRequestWithCorrectData()
    {
        // Arrange

        // Act
        var data = await _httpClient.GetAndReturnResponseAsync<int>("/User/GetUsersCount");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
        data.Value.Should().Be(4);
    }
}