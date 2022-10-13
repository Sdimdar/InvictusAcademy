using DataTransferLib.Models;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;
using SessionGatewayService.Application.Contracts;
using SessionGatewayService.Infrastructure.Extensions;
using System.Net.Http.Json;

namespace SessionGatewayService.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<DefaultResponceObject<string>> EditAsync(EditCommand command, CancellationToken cancellationToken)
    {
        var responce = await _httpClient.PostAsJsonAsync("/User/Edit", command, cancellationToken);
        return await responce.ReadContentAs<DefaultResponceObject<string>>();
    }

    public async Task<DefaultResponceObject<UserVm>> GetUserAsync(string email, CancellationToken cancellationToken)
    {
        var responce = await _httpClient.GetAsync($"/User/GetUserData?email={email}", cancellationToken);
        return await responce.ReadContentAs<DefaultResponceObject<UserVm>>();
    }

    public async Task<DefaultResponceObject<RegisterVm>> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken)
    {

        var responce = await _httpClient.PostAsJsonAsync("/User/Register", command, cancellationToken);
        return await responce.ReadContentAs<DefaultResponceObject<RegisterVm>>();
    }
}
