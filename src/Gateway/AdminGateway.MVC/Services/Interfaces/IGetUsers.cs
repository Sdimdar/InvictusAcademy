using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;

namespace AdminGateway.MVC.Services.Interfaces;

public interface IGetUsers : IUseExtendedHttpClient<IGetUsers>
{
    Task<DefaultResponseObject<UsersVm>> GetUsersAsync(int pageNumber, int pageSize);
    Task<DefaultResponseObject<string>> ChangeBanStatusAsync(ToBanCommand command);
    Task<DefaultResponseObject<int>> GetUsersCount();
}