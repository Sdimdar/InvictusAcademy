using Ardalis.Result;
using SessionGatewayService.Application.Contracts;
using SessionGatewayService.Domain.Entities;
using SessionGatewayService.Infrastructure.Extensions;

namespace SessionGatewayService.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<UserVm> GetUserAsync(string email, CancellationToken cancellationToken)
    {
        var responce = await _httpClient.GetAsync($"/user/getUserData?email={email}", cancellationToken);
        return await responce.ReadContentAs<Result<UserVm>>();
    }
}
