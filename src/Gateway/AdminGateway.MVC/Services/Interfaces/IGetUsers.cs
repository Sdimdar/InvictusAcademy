using DataTransferLib.Models;
using ServicesContracts.Identity.Responses;

namespace AdminGateway.MVC.Services.Interfaces;

public interface IGetUsers
{
    Task<DefaultResponseObject<UsersVm>> GetUsersAsync();
}