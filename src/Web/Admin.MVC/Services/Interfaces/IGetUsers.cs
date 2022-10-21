using Admin.MVC.ViewModels;
using DataTransferLib.Models;
using ServicesContracts.Identity.Responses;

namespace Admin.MVC.Services.Interfaces;

public interface IGetUsers
{
    Task<DefaultResponceObject<UsersVm>> GetUsersAsync();
}