using System.Security.Claims;
using AdminGateway.MVC.Models;
using AdminGateway.MVC.Models.DbModels;
using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using Ardalis.Result;
using Microsoft.AspNetCore.Identity;


namespace AdminGateway.MVC.Services;

public class AdminService : IAdminService
{
    private readonly UserManager<AdminUser> _adminManager;
    private readonly SignInManager<AdminUser> _signInManager;

    public AdminService(UserManager<AdminUser> adminManager, 
        SignInManager<AdminUser> signInManager)
    {
        _adminManager = adminManager;
        _signInManager = signInManager;
    }

    public async Task<Result<AdminUser>> LoginAdminAsync(LoginViewModel request, CancellationToken cancellationToken)
    {
        var admin = await _adminManager.FindByEmailAsync(request.Login);

        var result = await _signInManager.PasswordSignInAsync(
            admin,
            request.Password,
            false,
            false);

        if (result.Succeeded)
        {
            return Result.Success(admin);
        }

        return Result.Error("Invalid password, login pair match");
    }
    
    public async Task<Result<AdminUser>> GetAdminDataAsync(ClaimsPrincipal user, CancellationToken cancellationToken)
    {
        var admin = await _adminManager.GetUserAsync(user);
        if (admin is not null)
        {
            return Result.Success(admin);
        }
        
        return Result.Error("User is not Authorized");
        
    }

    public async Task<Result> LogoutAdminAsync()
    {
        await _signInManager.SignOutAsync();
        return Result.Success();
    }

    public async Task<Result<bool>> CreateNewAdmin(CreateAdminVm model, CancellationToken cancellationToken)
    {
        var admin = new AdminUser
        {
            UserName = model.UserName,
            Email = model.UserName
        };
        var result = await _adminManager.CreateAsync(admin, model.Password);
        if (result.Succeeded)
        {
            foreach (var role in model.Roles)
            {
                await _adminManager.AddToRoleAsync(admin, role);
            }

            return Result.Success(true);
        }

        return false;
    }
}