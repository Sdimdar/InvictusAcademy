using Ardalis.Result;
using DataTransferLib.Models;
using Newtonsoft.Json;
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

    public async Task<DefaultResponceObject<UserVm>> GetUserAsync(string email, CancellationToken cancellationToken)
    {
        var responce = await _httpClient.GetAsync($"/user/getUserData?email={email}", cancellationToken);
        string dataAsString = await responce.Content.ReadAsStringAsync().ConfigureAwait(false);
        return await responce.ReadContentAs<DefaultResponceObject<UserVm>>();
    }
}
