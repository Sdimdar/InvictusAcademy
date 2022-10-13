using DataTransferLib.Models;
using ServicesContracts.Request.Requests.Commands;

namespace SessionGatewayService.Application.Contracts;

public interface IRequestService
{
    Task<DefaultResponceObject<string>> CreateResponceAsync(CreateRequestCommand command, CancellationToken cancellationToken);
}
