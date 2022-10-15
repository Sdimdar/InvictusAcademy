using Identity.Domain.Entities;
using Identity.Infrastructure.Persistance;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServicesContracts.Identity.Requests.Commands;

namespace Identity.API.Tests;

[TestFixture]
public class EditTests
{
    private readonly List<UserDbModel> _users;
    private readonly HttpClient _client;

    public EditTests()
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
                        options.UseInMemoryDatabase("identity_db");
                    });
                });
            });

        var dbContext = application.Services.CreateScope().ServiceProvider.GetService<IdentityDbContext>();
        dbContext!.Users.AddRange(_users);
        dbContext!.SaveChanges();

        _client = application.CreateClient();
    }

    [Test]
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
        var response = await _client.PostAsJsonAsync("/User/Edit", command);
        DefaultResponceObject<string>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<string>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(data, Is.Not.Null);
        Assert.That(data.IsSuccess, Is.EqualTo(true));
    }

    [Test]
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
        var response = await _client.PostAsJsonAsync("/User/Edit", command);
        DefaultResponceObject<string>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<string>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(data, Is.Not.Null);
        Assert.That(data.IsSuccess, Is.EqualTo(false));
        Assert.That(data.Errors, Is.EqualTo(new string[] { "An error occurred while creating the request" }));
    }

    private static readonly string[] _invalidNumbers = { "12931", "asd", "" };

    [Test]
    [TestCaseSource(nameof(_invalidNumbers))]
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
        var response = await _client.PostAsJsonAsync("/User/Edit", command);
        DefaultResponceObject<string>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<string>>(dataAsString);
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
        var response = await _client.PostAsJsonAsync("/User/Edit", command);
        DefaultResponceObject<string>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<string>>(dataAsString);
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
        var response = await _client.PostAsJsonAsync("/User/Edit", command);
        DefaultResponceObject<string>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<string>>(dataAsString);
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
        var response = await _client.PostAsJsonAsync("/User/Edit", command);
        DefaultResponceObject<string>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<string>>(dataAsString);
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
        var response = await _client.PostAsJsonAsync("/User/Edit", command);
        DefaultResponceObject<string>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<string>>(dataAsString);
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
        var response = await _client.PostAsJsonAsync("/User/Edit", command);
        DefaultResponceObject<string>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<string>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(data, Is.Not.Null);
        Assert.That(data.IsSuccess, Is.EqualTo(false));
        Assert.That(data.ValidationErrors, Is.Not.Null);
        Assert.That(data.ValidationErrors.FirstOrDefault(e => e.Identifier == "Citizenship"), Is.Not.Null);
    }
}
