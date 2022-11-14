using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.Request.Requests.Commands;

namespace UserGateway.Application.Contracts;

public interface IRequestService : IUseExtendedHttpClient<IRequestService>
{
    Task<DefaultResponseObject<string>> CreateResponseAsync(CreateRequestCommand command, CancellationToken cancellationToken);
}
