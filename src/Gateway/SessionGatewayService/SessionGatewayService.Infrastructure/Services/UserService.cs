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

    public async Task<DefaultResponseObject<string>> EditAsync(EditCommand command, CancellationToken cancellationToken)
    {
        var responce = await _httpClient.PostAsJsonAsync("/User/Edit", command, cancellationToken);
        return await responce.ReadContentAs<DefaultResponseObject<string>>();
    }

    public async Task<DefaultResponseObject<UserVm>> GetUserAsync(string email, CancellationToken cancellationToken)
    {
        var responce = await _httpClient.GetAsync($"/User/GetUserData?email={email}", cancellationToken);
        return await responce.ReadContentAs<DefaultResponseObject<UserVm>>();
    }

    public async Task<DefaultResponseObject<RegisterVm>> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken)
    {

        var responce = await _httpClient.PostAsJsonAsync("/User/Register", command, cancellationToken);
        return await responce.ReadContentAs<DefaultResponseObject<RegisterVm>>();
    }
}
