using DataTransferLib.Models;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;

namespace AdminGateway.MVC.Services.Interfaces;

public interface IGetUsers
{
    Task<DefaultResponseObject<UsersVm>> GetUsersAsync(int pageNumber, int pageSize);
    Task<DefaultResponseObject<string>> ChangeBanStatusAsync(ToBanCommand command);
}