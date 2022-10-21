using Admin.MVC.ViewModels;

namespace Admin.MVC.Services.Interfaces;

public interface IAdminCreate
{
    Task<bool> CreateNewAdmin(CreateAdminVm model);
}