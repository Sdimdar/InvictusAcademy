using DataTransferLib.Models;
using ServicesContracts.Request.Requests.Commands;

namespace UserGateway.Application.Contracts;

public interface IRequestService
{
    Task<DefaultResponseObject<string>> CreateResponseAsync(CreateRequestCommand command,
        CancellationToken cancellationToken);
}
