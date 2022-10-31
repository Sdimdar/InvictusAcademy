using DataTransferLib.Models;
using ExtendedHttpClient;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.Request.Requests.Commands;
using UserGateway.Application.Contracts;

namespace UserGateway.Infrastructure.Services;

public class RequestService :IUseExtendedHttpClient<RequestService>, IRequestService
{
    public ExtendedHttpClient<RequestService> ExtendedHttpClient { get; set; }
    public RequestService(ExtendedHttpClient<RequestService> extendedHttpClient)
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
