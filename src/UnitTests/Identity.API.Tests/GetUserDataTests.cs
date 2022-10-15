using Identity.Domain.Entities;
using Identity.Infrastructure.Persistance;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServicesContracts.Identity.Responses;

namespace Identity.API.Tests;

[TestFixture]
public class GetUserDataTests
{
    private readonly List<UserDbModel> _users;
    private readonly HttpClient _client;
        
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
        dbContext!.Users.AddRange(_users);
        dbContext!.SaveChanges();

        _client = application.CreateClient();
    }

    [Test]
    public async Task GetUserData_SendRequestWithCorrectData()
    {
        // Arrange
        string email = "test@test.ru";

        // Act
        var response = await _client.GetAsync($"/User/GetUserData?email={email}");
        DefaultResponceObject<UserVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<UserVm>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(data, Is.Not.Null);
        Assert.That(data.IsSuccess, Is.EqualTo(true));
        Assert.That(data.Value, Is.Not.Null);
        Assert.That(data.Value.Email, Is.EqualTo(email));
    }

    private static readonly string[] _invalidEmails = { "testi@mail.ru", "asd" };

    [Test]
    [TestCaseSource(nameof(_invalidEmails))]
    public async Task GetUserData_SendRequestWithInvalidEmail(string invalidEmail)
    {
        // Arrange

        // Act
        var response = await _client.GetAsync($"/User/GetUserData?email={invalidEmail}");
        DefaultResponceObject<UserVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<UserVm>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(data, Is.Not.Null);
        Assert.That(data.IsSuccess, Is.EqualTo(false));
        Assert.That(data.Errors, Is.Not.Null);
    }

    [Test]
    public async Task GetUserData_SendRequestWithInvalidEmail()
    {
        // Arrange
        string invalidEmail = "";

        // Act
        var response = await _client.GetAsync($"/User/GetUserData?email={invalidEmail}");
        DefaultResponceObject<UserVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<UserVm>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
}
