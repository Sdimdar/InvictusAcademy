using DataTransferLib.Models;
using ServicesContracts.Request.Requests.Commands;
using System.Net.Http.Json;
using UserGateway.Application.Contracts;
using UserGateway.Infrastructure.Extensions;

namespace UserGateway.Infrastructure.Services;

public class RequestService : IRequestService
{
    private readonly HttpClient _httpClient;

    public RequestService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<DefaultResponseObject<string>> CreateResponseAsync(CreateRequestCommand command, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("/Request/Create", command, cancellationToken);
        return await response.ReadContentAs<DefaultResponseObject<string>>();
    }
}
