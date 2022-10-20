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
    
    public async Task<RegisteredUsersVM> GetUsersAsync()
    {
        var response = await _httpClient.GetAsync("/User/GetUsersData");
        return await response.ReadContentAs<RegisteredUsersVM>();
    }

}