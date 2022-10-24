using AdminGateway.MVC.HttpClientExtensions;
using AdminGateway.MVC.Services.Interfaces;
using AutoMapper;
using DataTransferLib.Models;
using ServicesContracts.Identity.Responses;

namespace AdminGateway.MVC.Services;

public class GetUsers : IGetUsers
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public GetUsers(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<DefaultResponseObject<UsersVm>> GetUsersAsync()
    {
        var response = await _httpClient.GetAsync("/User/GetUsersData");
        return await response.ReadContentAs<DefaultResponseObject<UsersVm>>();
    }

}