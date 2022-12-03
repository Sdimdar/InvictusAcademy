using AdminGateway.MVC.Services.Interfaces;
using DataTransferLib.Models;
using ExtendedHttpClient;
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

    public async Task<DefaultResponseObject<int>> GetUsersCount()
    {
        return await ExtendedHttpClient
            .GetAndReturnResponseAsync<DefaultResponseObject<int>>("/User/GetUsersCount");
    }

    public async Task<DefaultResponseObject<UsersVm>> GetUsersAsync(int pageNumber, int pageSize)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<UsersVm>>(
            $"/User/GetAllRegisteredUsersData?pageNumber={pageNumber}&pageSize={pageSize}");
    }

    public async Task<DefaultResponseObject<string>> ChangeBanStatusAsync(ToBanCommand command)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<ToBanCommand, DefaultResponseObject<string>>(command,
            "/User/SetBanStatus");
    }

}