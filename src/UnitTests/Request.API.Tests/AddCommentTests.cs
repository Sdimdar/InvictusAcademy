using ServicesContracts.Request.Requests.Commands;

namespace Request.API.Tests;

public class AddCommentTests : IClassFixture<CustomApplicationFactory<Program>>
{
    private readonly HttpClientWrapper _httpClient;
    private readonly CustomApplicationFactory<Program> _factory;
    public AddCommentTests(CustomApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = new HttpClientWrapper(_factory.CreateClient());
    }

    [Fact]
    public async Task AddComment_SendRequestWithCorrectData()
    {
        // Arrange
        ManagerCommentCommand command = new()
        {
            Id = 1,
            ManagerComment = "sadfasdfas"
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<ManagerCommentCommand, string>(command, "/Request/AddComment");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task AddComment_SendRequestWithInvalidId()
    {
        // Arrange
        ManagerCommentCommand command = new()
        {
            Id = 0,
            ManagerComment = "sadfasdfas"
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<ManagerCommentCommand, string>(command, "/Request/AddComment");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.Errors.Should().NotBeNull();
    }

    [Fact]
    public async Task AddComment_SendRequestWithInvalidComment()
    {
        // Arrange
        ManagerCommentCommand command = new()
        {
            Id = 1,
            ManagerComment = new string('s', 101)
        };

        // Act
        var data = await _httpClient.PostAndReturnResponseAsync<ManagerCommentCommand, string>(command, "/Request/AddComment");

        // Assert
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.ValidationErrors.Should().NotBeNull();
        data.ValidationErrors.Should().Contain(e => e.Identifier == "ManagerComment");
    }
}
