using DataTransferLib.Models;
using ServicesContracts.Request.Requests.Commands;
using DataTransferLib.Interfaces;
using UserGateway.Application.Contracts;

namespace UserGateway.Infrastructure.Services;

public class RequestService : IRequestService
{
    private readonly IHttpClientWrapper _httpClient;

    public RequestService(HttpClient httpClient, IHttpClientWrapper httpClientWrapper)
    {
        _httpClient = httpClientWrapper ?? throw new ArgumentNullException(nameof(httpClientWrapper));
        _httpClient.HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<DefaultResponseObject<string>> CreateResponseAsync(CreateRequestCommand command, CancellationToken cancellationToken)
    {
        return await _httpClient.PostAndReturnResponseAsync<CreateRequestCommand, string>(command, "/Request/Create", cancellationToken);
    }
}
