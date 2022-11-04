using AdminGateway.MVC.Services.Interfaces;
using DataTransferLib.Models;
using ExtendedHttpClient;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;

namespace AdminGateway.MVC.Services;

public class GetUsers : IGetUsers
{
    public ExtendedHttpClient<IGetUsers> ExtendedHttpClient { get; set; }

    public GetUsers(ExtendedHttpClient<IGetUsers> extendedHttpClient)
    {
        ExtendedHttpClient = extendedHttpClient;
    }
    
    public async Task<DefaultResponseObject<UsersVm>> GetUsersAsync()
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<UsersVm>>("/User/GetAllRegisteredUsersData");
    }

    public async Task<DefaultResponseObject<UsersVm>> GetUsersAsync(int pageNumber, int pageSize)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<UsersVm>>(
            $"/User/GetUsersCount?pageNumber={pageNumber}&pageSize={pageSize}");
    }

    public async Task<DefaultResponseObject<string>> ChangeBanStatusAsync(ToBanCommand command)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<ToBanCommand, DefaultResponseObject<string>>(command,
            "/User/SetBanStatus");
    }

}