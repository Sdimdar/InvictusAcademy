using AdminGateway.MVC.HttpClientExtensions;
using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using Ardalis.Result;
using DataTransferLib.Models;

namespace AdminGateway.MVC.Services;

public class ModuleService : IModuleService
{
    private readonly HttpClient _httpClient;

    public ModuleService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<DefaultResponseObject<bool>> CreateNewModule(CreateModuleVM request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync($"/Modules/Create", request);
        return await response.ReadContentAs<DefaultResponseObject<bool>>();
    }
}