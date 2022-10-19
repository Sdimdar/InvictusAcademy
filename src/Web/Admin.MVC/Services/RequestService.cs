using Admin.MVC.HttpClientExtensions;
using Admin.MVC.Services.Interfaces;
using Admin.MVC.ViewModels;
using DataTransferLib.Mappings;
using DataTransferLib.Models;
using ServicesContracts.Identity.Responses;
using ServicesContracts.Request.Requests.Commands;
using ServicesContracts.Request.Responses;

namespace Admin.MVC.Services;

public class RequestService : IRequestService
{
    private readonly HttpClient _httpClient;

    public RequestService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<DefaultResponceObject<GetAllRequestVm>> GetAllRequestsAsync(int pageNumber, int pageSize)
    {
        var responce = await _httpClient.GetAsync($"/Request/GetAll?pageNumber={pageNumber}&pageSize={pageSize}");
        return await responce.ReadContentAs<DefaultResponceObject<GetAllRequestVm>>();
    }

    public async Task<DefaultResponceObject<string>> ChangeCalledStatusAsync(ChangeCalledStatusCommand command)
    {
        var responce = await _httpClient.PostAsJsonAsync($"/Request/SetCalledStatus", command);
        return await responce.ReadContentAs<DefaultResponceObject<string>>();
    }

    public async Task<DefaultResponceObject<string>> ManagerCommentAsync(ManagerCommentCommand request)
    {
        var responce = await _httpClient.PostAsJsonAsync($"/Request/AddComment", request);
        return await responce.ReadContentAs<DefaultResponceObject<string>>();
    }

    public async Task<DefaultResponceObject<int>> GetRequestsCountAsync()
    {
        var responce = await _httpClient.GetAsync($"/Request/Count");
        return await responce.ReadContentAs<DefaultResponceObject<int>>();
    }
}