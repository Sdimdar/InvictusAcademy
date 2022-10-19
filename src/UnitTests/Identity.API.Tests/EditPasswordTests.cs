using Identity.API.Tests.Repository;
using Identity.Application.Contracts;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using ServicesContracts.Identity.Requests.Commands;

namespace Identity.API.Tests;

public class EditPasswordTests
{
    private readonly List<UserDbModel> _users;
    private readonly HttpClient _httpClient;

    public EditPasswordTests()
    {
        _users = new()
        {
            new UserDbModel()
            {
                Id = 1,
                AvatarLink = null,
                Citizenship = "Казахстан",
                Email = "test@mail.ru",
                FirstName = "Famine",
                MiddleName = "Famine",
                LastName = "Famine",
                InstagramLink = null,
                PhoneNumber = "82739348372",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Password = "ADUW344ryoKSA8iyKGjTgYWHVpTj1u9stDjCxRCj28Uppq0ujck4iv3gUkTMvrBRlQ==", // 123_QWEasd
                RegistrationDate = DateTime.Now
            }
        };

        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var repositoryDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IUserRepository));
                    services.Remove(repositoryDescriptor!);
                    services.AddSingleton<IUserRepository, UserMockRepository>();
                });
            });

        var repository = application.Services.CreateScope().ServiceProvider.GetService<IUserRepository>();
        if (repository is UserMockRepository userMockRepository)
        {
            userMockRepository.InitialData(_users);
        }

        _httpClient = application.CreateClient();
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
        var response = await _httpClient.PostAsJsonAsync("/User/EditPassword", command);
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
        var response = await _httpClient.PostAsJsonAsync("/User/EditPassword", command);
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
        var response = await _httpClient.PostAsJsonAsync("/User/EditPassword", command);
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
        data.ValidationErrors.Should().Contain(e => e.Identifier == "NewPassword");
    }
}
