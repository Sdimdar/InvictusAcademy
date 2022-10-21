using Admin.MVC.Extensions;
using Admin.MVC.Services.Interfaces;
using Admin.MVC.ViewModels;
using AutoMapper;
using DataTransferLib.Models;
using ServicesContracts.Identity.Responses;

namespace Admin.MVC.Services;

public class GetUsers : IGetUsers
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public GetUsers(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<DefaultResponceObject<UsersVm>> GetUsersAsync()
    {
        var response = await _httpClient.GetAsync("/User/GetUsersData");
        return await response.ReadContentAs<DefaultResponceObject<UsersVm>>();
    }

}