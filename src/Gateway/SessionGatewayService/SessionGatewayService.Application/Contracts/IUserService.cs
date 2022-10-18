using DataTransferLib.Models;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;

namespace SessionGatewayService.Application.Contracts;

public interface IUserService
{
    Task<DefaultResponceObject<UserVm>> GetUserAsync(string email, CancellationToken cancellationToken);
    Task<DefaultResponceObject<RegisterVm>> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken);
    Task<DefaultResponceObject<string>> EditAsync(EditCommand command, CancellationToken cancellationToken);
    Task<DefaultResponceObject<string>> EditPasswordAsync(EditPasswordCommand command, CancellationToken cancellationToken);
}
