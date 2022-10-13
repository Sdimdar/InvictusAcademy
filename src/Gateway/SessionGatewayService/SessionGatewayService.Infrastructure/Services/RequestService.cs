using DataTransferLib.Models;
using ServicesContracts.Request.Requests.Commands;
using SessionGatewayService.Application.Contracts;
using SessionGatewayService.Infrastructure.Extensions;
using System.Net.Http.Json;

namespace SessionGatewayService.Infrastructure.Services;

public class RequestService : IRequestService
{
    private readonly HttpClient _httpClient;

    public RequestService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<DefaultResponceObject<string>> CreateResponceAsync(CreateRequestCommand command, CancellationToken cancellationToken)
    {
        var responce = await _httpClient.PostAsJsonAsync("/Request/Create", command, cancellationToken);
        return await responce.ReadContentAs<DefaultResponceObject<string>>();
    }
}
