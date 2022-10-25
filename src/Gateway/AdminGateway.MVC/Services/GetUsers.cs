using AdminGateway.MVC.HttpClientExtensions;
using AdminGateway.MVC.Services.Interfaces;
using AutoMapper;
using DataTransferLib.Models;
using ServicesContracts.Identity.Responses;

namespace AdminGateway.MVC.Services;

public class GetUsers : IGetUsers
{
    private readonly HttpClient _httpClient;

    public GetUsers(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    

    public async Task<DefaultResponseObject<UsersVm>> GetUsersAsync(int pageNumber, int pageSize)
    {
        var response = await _httpClient.GetAsync($"/User/GetAllRegisteredUsersData?pageNumber={pageNumber}&pageSize={pageSize}");
        return await response.ReadContentAs<DefaultResponseObject<UsersVm>>();
    }

    public async Task<DefaultResponseObject<int>> GetUsersCountAsync()
    {
        var response = await _httpClient.GetAsync($"/User/GetUsersCount");
        return await response.ReadContentAs<DefaultResponseObject<int>>();
    }
}