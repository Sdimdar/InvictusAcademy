using AdminGateway.MVC.HttpClientExtensions;
using AdminGateway.MVC.Services.Interfaces;
using DataTransferLib.Models;
using ServicesContracts.Request.Requests.Commands;
using ServicesContracts.Request.Responses;

namespace AdminGateway.MVC.Services;

public class RequestService : IRequestService
{
    private readonly HttpClient _httpClient;

    public RequestService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<DefaultResponseObject<GetAllRequestVm>> GetAllRequestsAsync(int pageNumber, int pageSize)
    {
        var response = await _httpClient.GetAsync($"/Request/GetAll?pageNumber={pageNumber}&pageSize={pageSize}");
        return await response.ReadContentAs<DefaultResponseObject<GetAllRequestVm>>();
    }

    public async Task<DefaultResponseObject<string>> ChangeCalledStatusAsync(ChangeCalledStatusCommand command)
    {
        var response = await _httpClient.PostAsJsonAsync($"/Request/SetCalledStatus", command);
        return await response.ReadContentAs<DefaultResponseObject<string>>();
    }

    public async Task<DefaultResponseObject<string>> ManagerCommentAsync(ManagerCommentCommand request)
    {
        var response = await _httpClient.PostAsJsonAsync($"/Request/AddComment", request);
        return await response.ReadContentAs<DefaultResponseObject<string>>();
    }

    public async Task<DefaultResponseObject<int>> GetRequestsCountAsync()
    {
        var response = await _httpClient.GetAsync($"/Request/Count");
        return await response.ReadContentAs<DefaultResponseObject<int>>();
    }
}