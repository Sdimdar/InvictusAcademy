using DataTransferLib.Models;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;

namespace SessionGatewayService.Application.Contracts;

public interface IUserService
{
    Task<DefaultResponseObject<UserVm>> GetUserAsync(string email, CancellationToken cancellationToken);
    Task<DefaultResponseObject<RegisterVm>> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken);
    Task<DefaultResponseObject<string>> EditAsync(EditCommand command, CancellationToken cancellationToken);
}
