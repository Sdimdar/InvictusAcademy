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
        var responce = await _httpClient.GetAsync($"/Request/GetAll?pageNumber={pageNumber}&pageSize={pageSize}");
        return await responce.ReadContentAs<DefaultResponseObject<GetAllRequestVm>>();
    }

    public async Task<DefaultResponseObject<string>> ChangeCalledStatusAsync(ChangeCalledStatusCommand command)
    {
        var responce = await _httpClient.PostAsJsonAsync($"/Request/SetCalledStatus", command);
        return await responce.ReadContentAs<DefaultResponseObject<string>>();
    }

    public async Task<DefaultResponseObject<string>> ManagerCommentAsync(ManagerCommentCommand request)
    {
        var responce = await _httpClient.PostAsJsonAsync($"/Request/AddComment", request);
        return await responce.ReadContentAs<DefaultResponseObject<string>>();
    }

    public async Task<DefaultResponseObject<int>> GetRequestsCountAsync()
    {
        var responce = await _httpClient.GetAsync($"/Request/Count");
        return await responce.ReadContentAs<DefaultResponseObject<int>>();
    }
}