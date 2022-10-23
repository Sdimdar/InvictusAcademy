﻿using DataTransferLib.Models;
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
        var response = await _httpClient.PostAsJsonAsync("/User/Edit", command, cancellationToken);
        return await response.ReadContentAs<DefaultResponseObject<string>>();
    }

    public async Task<DefaultResponseObject<string>> EditPasswordAsync(EditPasswordCommand command, 
        CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("/User/EditPassword", 
            command, cancellationToken);
        return await response.ReadContentAs<DefaultResponseObject<string>>();
    }

    public async Task<DefaultResponseObject<UserVm>> GetUserAsync(string email, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"/User/GetUserData?email={email}", cancellationToken);
        return await response.ReadContentAs<DefaultResponseObject<UserVm>>();
    }

    public async Task<DefaultResponseObject<RegisterVm>> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken)
    {

        var response = await _httpClient.PostAsJsonAsync("/User/Register", command, cancellationToken);
        return await response.ReadContentAs<DefaultResponseObject<RegisterVm>>();
    }
}
