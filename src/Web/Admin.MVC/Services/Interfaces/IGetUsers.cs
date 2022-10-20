using Admin.MVC.ViewModels;

namespace Admin.MVC.Services.Interfaces;

public interface IGetUsers
{
    Task<RegisteredUsersVM> GetUsersAsync();
}