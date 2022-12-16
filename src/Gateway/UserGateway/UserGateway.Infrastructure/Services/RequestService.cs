using DataTransferLib.Models;
using ExtendedHttpClient;
using ServicesContracts.Request.Requests.Commands;
using UserGateway.Application.Contracts;

namespace UserGateway.Infrastructure.Services;

public class RequestService : IRequestService
{
    public ExtendedHttpClient<IRequestService> ExtendedHttpClient { get; set; }
    public RequestService(ExtendedHttpClient<IRequestService> extendedHttpClient)
    {
        ExtendedHttpClient = extendedHttpClient;
    }

    public async Task<DefaultResponseObject<string>> CreateResponseAsync(CreateRequestCommand command, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient
            .PostAndReturnResponseAsync<CreateRequestCommand, DefaultResponseObject<string>>(command,
                "/Request/Create", cancellationToken);
    }


}
