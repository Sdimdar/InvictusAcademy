using AdminGateway.MVC.ViewModels;

namespace AdminGateway.MVC.Services.Interfaces
{
    public interface IAdminCreate
    {
        Task<bool> CreateNewAdmin(CreateAdminVm model);
    }
}