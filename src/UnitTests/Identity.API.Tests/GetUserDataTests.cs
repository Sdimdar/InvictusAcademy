using Identity.Domain.Entities;
using Identity.Infrastructure.Persistance;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServicesContracts.Identity.Responses;

namespace Identity.API.Tests;

public class GetUserDataTests
{
    private readonly List<UserDbModel> _users;
    private readonly HttpClient _httpClient;

    public GetUserDataTests()
    {
        _users = new()
        {
            new UserDbModel()
            {
                Id = 1,
                AvatarLink = null,
                Citizenship = "Казахстан",
                Email = "test@test.ru",
                FirstName = "Famine",
                MiddleName = "Famine",
                LastName = "Famine",
                InstagramLink = null,
                PhoneNumber = "82739234234",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Password = "asdfhadjkfhakjsdfhladhfkadhsjhad",
                RegistrationDate = DateTime.Now
            }
        };

        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<IdentityDbContext>));
                    services.Remove(dbContextDescriptor!);
                    services.AddDbContext<IdentityDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("identity_getuserdata_db");
                    });
                });
            });

        var dbContext = application.Services.CreateScope().ServiceProvider.GetService<IdentityDbContext>();
        if (dbContext.Users.Count() == 0)
        {
            dbContext!.Users.AddRange(_users);
            dbContext!.SaveChanges();
        }

        _httpClient = application.CreateClient();
    }

    [Theory]
    [InlineData("test@test.ru")]
    public async Task GetUserData_SendRequestWithCorrectData(string email)
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync($"/User/GetUserData?email={email}");
        DefaultResponceObject<UserVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<UserVm>>(dataAsString);
        }

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
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
        var response = await _httpClient.GetAsync($"/User/GetUserData?email={invalidEmail}");
        DefaultResponceObject<UserVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<UserVm>>(dataAsString);
        }

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
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
