using Identity.API.Tests.Repository;
using Identity.Application.Contracts;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistance;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServicesContracts.Identity.Responses;

namespace Identity.API.Tests;

public class GetUsersDataTests
{
    private readonly List<UserDbModel> _users;
    private readonly HttpClient _httpClient;

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

    public static IEnumerable<object[]> CorrectDataWithoutFilter()
    {
        yield return new object[] { 1, 2 };
        yield return new object[] { 1, 5 };
        yield return new object[] { 2, 3 };
        yield return new object[] { 1, 100 };
    }

    [Theory]
    [MemberData(nameof(CorrectDataWithoutFilter))]
    public async Task GetUsersData_SendRequestWithCorrectDataWithoutFilter(int page, int pageSize)
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync($"/User/GetUsersData?Page={page}&PageSize={pageSize}");
        DefaultResponseObject<UsersVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponseObject<UsersVm>>(dataAsString);
        }

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
        data.Value.Should().NotBeNull();
        data.Value.Users.Count().Should().Be(_users.Count - pageSize * (page - 1) < pageSize ? _users.Count - pageSize * (page - 1) : pageSize);
        data.Value.PageVm.PageNumber.Should().Be(page);
        data.Value.PageVm.TotalPages.Should().Be((int)Math.Ceiling(_users.Count / (double)pageSize));
    }

    public static IEnumerable<object[]> CorrectDataWithFilter()
    {
        yield return new object[] { 1, 2, "Famine" };
        yield return new object[] { 2, 3, "test" };
        yield return new object[] { 1, 5, "Famine" };
        yield return new object[] { 1, 100, "test" };
    }

    [Theory]
    [MemberData(nameof(CorrectDataWithFilter))]
    public async Task GetUsersData_SendRequestWithCorrectDataWithFilter(int page, int pageSize, string filter)
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync($"/User/GetUsersData?Page={page}&PageSize={pageSize}&FilterString={filter}");
        DefaultResponseObject<UsersVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponseObject<UsersVm>>(dataAsString);
        }

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
        data.Value.Should().NotBeNull();
        data.Value.Users.Count().Should().Be(_users.Count - pageSize * (page - 1) < pageSize ? _users.Count - pageSize * (page - 1) : pageSize);
        data.Value.PageVm.PageNumber.Should().Be(page);
        data.Value.PageVm.TotalPages.Should().Be((int)Math.Ceiling(_users.Count / (double)pageSize));
    }


    [Fact]
    public async Task GetAllUsersData_SendRequestWithCorrectDataWithoutFilter()
    {
        // Arrange
        int page = 1;
        int pageSize = 0;

        // Act
        var response = await _httpClient.GetAsync($"/User/GetUsersData?Page={page}&PageSize={pageSize}");
        DefaultResponseObject<UsersVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponseObject<UsersVm>>(dataAsString);
        }

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeTrue();
        data.Value.Should().NotBeNull();
        data.Value.Users.Count().Should().Be(_users.Count);
        data.Value.PageVm.PageNumber.Should().Be(page);
        data.Value.PageVm.TotalPages.Should().Be(1);
    }

    public static IEnumerable<object[]> InvalidDataWithoutFilter()
    {
        yield return new object[] { -1, 2 };
        yield return new object[] { 0, 3 };
        yield return new object[] { 1, -100 };
    }

    [Theory]
    [MemberData(nameof(InvalidDataWithoutFilter))]
    public async Task GetUsersData_SendRequestWithWrongDataWithoutFilter(int page, int pageSize)
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync($"/User/GetUsersData?Page={page}&PageSize={pageSize}");
        DefaultResponseObject<UsersVm>? data = null;
        if (response.IsSuccessStatusCode)
        {
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            data = JsonConvert.DeserializeObject<DefaultResponseObject<UsersVm>>(dataAsString);
        }

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data.IsSuccess.Should().BeFalse();
        data.Value.Should().NotBeNull();
        data.Errors.Should().NotBeNull();
    }
}
