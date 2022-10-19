using Admin.MVC.Models.DbModels;
using Admin.MVC.Services.Interfaces;
using Admin.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Admin.MVC.Services;

public class CreateAdmin:IAdminCreate
{
    private readonly UserManager<User> _userManager;

    public CreateAdmin(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> CreateNewAdmin(CreateAdminVM model)
    {
        var user = new User
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