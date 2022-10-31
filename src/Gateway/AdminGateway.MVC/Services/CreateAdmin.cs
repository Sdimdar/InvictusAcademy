using AdminGateway.MVC.Models.DbModels;
using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace AdminGateway.MVC.Services;

public class CreateAdmin : IAdminCreate
{
    private readonly UserManager<AdminUser> _userManager;

    public CreateAdmin(UserManager<AdminUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> CreateNewAdmin(CreateAdminVm model)
    {
        var user = new AdminUser
        {
            UserName = model.UserName
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            foreach (var role in model.Roles)
            {
                if (role == "false")
                    continue;
                await _userManager.AddToRoleAsync(user, role);
            }

            return true;
        }

        return false;
    }
}