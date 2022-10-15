using Identity.Domain.Entities;
using Identity.Infrastructure.Persistance;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServicesContracts.Identity.Responses;

namespace Identity.API.Tests;

[TestFixture]
public class GetUsersDataTests
{
    private readonly List<UserDbModel> _users;
    private readonly HttpClient _client;

    public GetUsersDataTests()
    {
        #region Users Init
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
            },
            new UserDbModel()
            {
                Id = 2,
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
            },
            new UserDbModel()
            {
                Id = 3,
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
            },
            new UserDbModel()
            {
                Id = 4,
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
        #endregion

        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<IdentityDbContext>));
                    services.Remove(dbContextDescriptor!);
                    services.AddDbContext<IdentityDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("identity_getusersdata_db");
                    });
                });
            });

        var dbContext = application.Services.CreateScope().ServiceProvider.GetService<IdentityDbContext>();
        dbContext!.Users.AddRange(_users);
        dbContext!.SaveChanges();

        _client = application.CreateClient();
    }

    private static readonly (int, int)[] _validPages = { (1, 2), (2, 3), (1, 5), (1, 100) };

    [Test]
    [TestCaseSource(nameof(_validPages))]
    public async Task GetUsersData_SendRequestWithCorrectDataWithoutFilter((int, int) pagesData)
    {
        // Arrange
        int page = pagesData.Item1;
        int pageSize = pagesData.Item2;

        // Act
        var response = await _client.GetAsync($"/User/GetUsersData?Page={page}&PageSize={pageSize}");
        DefaultResponceObject<UsersVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<UsersVm>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(data, Is.Not.Null);
        Assert.That(data.IsSuccess, Is.EqualTo(true));
        Assert.That(data.Value, Is.Not.Null);
        Assert.That(data.Value.Users.Count, Is.EqualTo(_users.Count - (pageSize * (page - 1)) < pageSize ? _users.Count - (pageSize * (page - 1)) : pageSize));
        Assert.That(data.Value.PageVm.PageNumber, Is.EqualTo(page));
        Assert.That(data.Value.PageVm.TotalPages, Is.EqualTo((int)Math.Ceiling(_users.Count / (double)pageSize)));
    }

    private static readonly (int, int, string)[] _validPagesWithFilter = 
        { 
            (1, 2, "Famine"), 
            (2, 3, "test"), 
            (1, 5, "Famine"), 
            (1, 100, "test") 
        };

    [Test]
    [TestCaseSource(nameof(_validPagesWithFilter))]
    public async Task GetUsersData_SendRequestWithCorrectDataWithFilter((int, int, string) pagesData)
    {
        // Arrange
        int page = pagesData.Item1;
        int pageSize = pagesData.Item2;
        string filter = pagesData.Item3;

        // Act
        var response = await _client.GetAsync($"/User/GetUsersData?Page={page}&PageSize={pageSize}&FilterString={filter}");
        DefaultResponceObject<UsersVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<UsersVm>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(data, Is.Not.Null);
        Assert.That(data.IsSuccess, Is.EqualTo(true));
        Assert.That(data.Value, Is.Not.Null);
        Assert.That(data.Value.Users.Count, Is.EqualTo(_users.Count - (pageSize * (page - 1)) < pageSize ? _users.Count - (pageSize * (page - 1)) : pageSize));
        Assert.That(data.Value.PageVm.PageNumber, Is.EqualTo(page));
        Assert.That(data.Value.PageVm.TotalPages, Is.EqualTo((int)Math.Ceiling(_users.Count / (double)pageSize)));
    }


    private static readonly (int, int)[] _invalidPages = { (-1, 2), (0, 3), (1, 0), (1, -100) };

    [Test]
    [TestCaseSource(nameof(_invalidPages))]
    public async Task GetUsersData_SendRequestWithWrongDataWithoutFilter((int, int) pagesData)
    {
        // Arrange
        int page = pagesData.Item1;
        int pageSize = pagesData.Item2;

        // Act
        var response = await _client.GetAsync($"/User/GetUsersData?Page={page}&PageSize={pageSize}");
        DefaultResponceObject<UsersVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponceObject<UsersVm>>(dataAsString);
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(data, Is.Not.Null);
        Assert.That(data.IsSuccess, Is.EqualTo(true));
        Assert.That(data.Value, Is.Null);
        Assert.That(data.Errors, Is.Not.Null);
    }
}
