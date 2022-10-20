using Admin.MVC.Extensions;
using Admin.MVC.Services.Interfaces;
using Admin.MVC.ViewModels;
using ServicesContracts.Identity.Responses;

namespace Admin.MVC.Services;

public class GetUsers : IGetUsers
{
    private readonly HttpClient _httpClient;

    public GetUsers(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    
    public async Task<RegisteredUsersVM> GetUsersAsync(CancellationToken cancellationToken, int page, int pageSize)
    {
        page = 1;
        pageSize = 20;
        var responce = await _httpClient.GetAsync("/User/GetUsersData", cancellationToken);
        return await responce.ReadContentAs<RegisteredUsersVM>();
    }

}