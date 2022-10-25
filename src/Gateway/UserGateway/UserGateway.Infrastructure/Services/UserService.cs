using DataTransferLib.Models;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;
using System.Net.Http.Json;
using DataTransferLib.Interfaces;
using UserGateway.Application.Contracts;
using UserGateway.Infrastructure.Extensions;

namespace UserGateway.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IHttpClientWrapper _httpClient;

    public UserService(HttpClient httpClient, IHttpClientWrapper httpClientWrapper)
    {
        _httpClient = httpClientWrapper ?? throw new ArgumentNullException(nameof(httpClientWrapper));
        _httpClient.HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<DefaultResponseObject<string>> EditAsync(EditCommand command, CancellationToken cancellationToken)
    {
        return await _httpClient.PostAndReturnResponseAsync<EditCommand, string>(command, "/User/Edit", cancellationToken);
    }

    public async Task<DefaultResponseObject<string>> EditPasswordAsync(EditPasswordCommand command,
        CancellationToken cancellationToken)
    {
        return await _httpClient.PostAndReturnResponseAsync<EditPasswordCommand, string>(command, "/User/EditPassword", cancellationToken);
    }

    public async Task<DefaultResponseObject<UserVm>> GetUserAsync(string email, CancellationToken cancellationToken)
    {
        return await _httpClient.GetAndReturnResponseAsync<UserVm>($"/User/GetUserData?email={email}", cancellationToken);
    }

    public async Task<DefaultResponseObject<RegisterVm>> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken)
    {
        return await _httpClient.PostAndReturnResponseAsync<RegisterCommand, RegisterVm>(command, "/User/Register", cancellationToken);
    }
}
