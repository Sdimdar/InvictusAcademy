using DataTransferLib.Models;
using ExtendedHttpClient;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;
using UserGateway.Application.Contracts;

namespace UserGateway.Infrastructure.Services;

public class UserService : IUserService
{
    public ExtendedHttpClient<IUserService> ExtendedHttpClient { get; set; }
    public UserService(ExtendedHttpClient<IUserService> extendedHttpClient)
    {
        ExtendedHttpClient = extendedHttpClient;
    }


    public async Task<DefaultResponseObject<string>> EditAsync(EditCommand command, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<EditCommand, DefaultResponseObject<string>>(command,
            "/User/Edit", cancellationToken);
    }

    public async Task<DefaultResponseObject<string>> EditPasswordAsync(EditPasswordCommand command,
        CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<EditPasswordCommand, DefaultResponseObject<string>>(
            command, "/User/EditPassword", cancellationToken);
    }

    public async Task<DefaultResponseObject<UserVm>> GetUserAsync(string email, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<UserVm>>(
            $"/User/GetUserData?email={email}", cancellationToken);
    }

    public async Task<DefaultResponseObject<RegisterVm>> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<RegisterCommand, DefaultResponseObject<RegisterVm>>(
            command, "/User/Register", cancellationToken);
    }


}
