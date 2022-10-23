using DataTransferLib.Models;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;

namespace UserGateway.Application.Contracts;

public interface IUserService
{
    Task<DefaultResponseObject<UserVm>> GetUserAsync(string email, CancellationToken cancellationToken);
    Task<DefaultResponseObject<RegisterVm>> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken);
    Task<DefaultResponseObject<string>> EditAsync(EditCommand command, CancellationToken cancellationToken);
    Task<DefaultResponseObject<string>> EditPasswordAsync(EditPasswordCommand command, CancellationToken cancellationToken);
}
