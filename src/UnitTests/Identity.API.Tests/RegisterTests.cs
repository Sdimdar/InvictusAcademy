using Identity.Domain.Entities;
using Identity.Infrastructure.Persistance;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;

namespace Identity.API.Tests;

[TestFixture]
public class RegisterTests
{
    private readonly List<UserDbModel> _users;
    private readonly HttpClient _client;

    public RegisterTests()
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
                        options.UseInMemoryDatabase("identity_register_db");
                    });
                });
            });

        var dbContext = application.Services.CreateScope().ServiceProvider.GetService<IdentityDbContext>();
        dbContext!.Users.AddRange(_users);
        dbContext!.SaveChanges();

        _client = application.CreateClient();
    }

    [Test]
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
            Citizenship = "Казахстан",
            PhoneNumber = "89233047662"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/User/Register", command);
        DefaultResponceObject<RegisterVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<RegisterVm>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(data, Is.Not.Null);
        Assert.That(data.IsSuccess, Is.EqualTo(true));
        Assert.That(data.Value, Is.Not.Null);
        Assert.That(data.Value.Email, Is.EqualTo(command.Email));
    }

    [Test]
    public async Task Register_SendRequestWithInvalidEmail()
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
            Citizenship = "Казахстан",
            PhoneNumber = "89233047662"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/User/Register", command);
        DefaultResponceObject<RegisterVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<RegisterVm>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(data, Is.Not.Null);
        Assert.That(data.IsSuccess, Is.EqualTo(false));
        Assert.That(data.Errors, Is.Not.Null);
    }

    private static readonly (string, string)[] _invalidPasswordPairs = 
        { 
            ("12341231", "12341231"), 
            ("sdfasdvae", "sdfasdvae"), 
            ("123_QWEasd", "123qweasd"), 
            ("a1_A", "a1_A"), 
            ("", ""),
        };

    [Test]
    [TestCaseSource(nameof(_invalidPasswordPairs))]
    public async Task Register_SendRequestWithInvalidPhoneNumber((string, string) invalidPasswordPair)
    {
        // Arrange
        RegisterCommand command = new()
        {
            Email = "new_test1@mail.ru",
            Password = invalidPasswordPair.Item1,
            PasswordConfirm = invalidPasswordPair.Item2,
            FirstName = "Famine",
            MiddleName = "Famine",
            LastName = "Famine",
            InstagramLink = null,
            Citizenship = "Казахстан",
            PhoneNumber = "89233047662"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/User/Register", command);
        DefaultResponceObject<RegisterVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<RegisterVm>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(data, Is.Not.Null);
        Assert.That(data.IsSuccess, Is.EqualTo(false));
        Assert.That(data.ValidationErrors, Is.Not.Null);
        Assert.That(data.ValidationErrors.FirstOrDefault(e => e.Identifier == "Password" || e.Identifier == "PasswordConfirm"), Is.Not.Null);
    }

    private static readonly string[] _invalidNumbers = { "12931", "asd", "" };

    [Test]
    [TestCaseSource(nameof(_invalidNumbers))]
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
            Citizenship = "Казахстан",
            PhoneNumber = invalidNumber
        };

        // Act
        var response = await _client.PostAsJsonAsync("/User/Register", command);
        DefaultResponceObject<RegisterVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<RegisterVm>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(data, Is.Not.Null);
        Assert.That(data.IsSuccess, Is.EqualTo(false));
        Assert.That(data.ValidationErrors, Is.Not.Null);
        Assert.That(data.ValidationErrors.FirstOrDefault(e => e.Identifier == "PhoneNumber"), Is.Not.Null);
    }


    private static readonly string[] _invalidFirstNames = { "", new string('s', 300) };

    [Test]
    [TestCaseSource(nameof(_invalidFirstNames))]
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
            Citizenship = "Казахстан",
            PhoneNumber = "89233047662"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/User/Register", command);
        DefaultResponceObject<RegisterVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<RegisterVm>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(data, Is.Not.Null);
        Assert.That(data.IsSuccess, Is.EqualTo(false));
        Assert.That(data.ValidationErrors, Is.Not.Null);
        Assert.That(data.ValidationErrors.FirstOrDefault(e => e.Identifier == "FirstName"), Is.Not.Null);
    }

    private static readonly string[] _invalidMiddleNames = { new string('s', 300) };

    [Test]
    [TestCaseSource(nameof(_invalidMiddleNames))]
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
            Citizenship = "Казахстан",
            PhoneNumber = "89233047662"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/User/Register", command);
        DefaultResponceObject<RegisterVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<RegisterVm>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(data, Is.Not.Null);
        Assert.That(data.IsSuccess, Is.EqualTo(false));
        Assert.That(data.ValidationErrors, Is.Not.Null);
        Assert.That(data.ValidationErrors.FirstOrDefault(e => e.Identifier == "MiddleName"), Is.Not.Null);
    }

    private static readonly string[] _invalidLastNames = { "", new string('s', 300) };

    [Test]
    [TestCaseSource(nameof(_invalidLastNames))]
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
            Citizenship = "Казахстан",
            PhoneNumber = "89233047662"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/User/Register", command);
        DefaultResponceObject<RegisterVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<RegisterVm>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(data, Is.Not.Null);
        Assert.That(data.IsSuccess, Is.EqualTo(false));
        Assert.That(data.ValidationErrors, Is.Not.Null);
        Assert.That(data.ValidationErrors.FirstOrDefault(e => e.Identifier == "LastName"), Is.Not.Null);
    }

    private static readonly string[] _invalidInstagramLinks = { new string('s', 300) };

    [Test]
    [TestCaseSource(nameof(_invalidInstagramLinks))]
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
            Citizenship = "Казахстан",
            PhoneNumber = "89233047662"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/User/Register", command);
        DefaultResponceObject<RegisterVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<RegisterVm>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(data, Is.Not.Null);
        Assert.That(data.IsSuccess, Is.EqualTo(false));
        Assert.That(data.ValidationErrors, Is.Not.Null);
        Assert.That(data.ValidationErrors.FirstOrDefault(e => e.Identifier == "InstagramLink"), Is.Not.Null);
    }

    private static readonly string[] _invalidCitizenships = { "", new string('s', 300) };

    [Test]
    [TestCaseSource(nameof(_invalidCitizenships))]
    public async Task Register_SendRequestWithInvalidCitizenship(string invalidCitizenship)
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
            Citizenship = invalidCitizenship,
            PhoneNumber = "89233047662"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/User/Register", command);
        DefaultResponceObject<RegisterVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<RegisterVm>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(data, Is.Not.Null);
        Assert.That(data.IsSuccess, Is.EqualTo(false));
        Assert.That(data.ValidationErrors, Is.Not.Null);
        Assert.That(data.ValidationErrors.FirstOrDefault(e => e.Identifier == "Citizenship"), Is.Not.Null);
    }
}
