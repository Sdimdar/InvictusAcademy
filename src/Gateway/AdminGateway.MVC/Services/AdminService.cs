using AdminGateway.MVC.Models;
using AdminGateway.MVC.Models.DbModels;
using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using Ardalis.Result;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace AdminGateway.MVC.Services;

public class AdminService : IAdminService
{
    private readonly UserManager<AdminUser> _adminManager;
    private readonly SignInManager<AdminUser> _signInManager;
    private readonly AdminDbContext _db;

    public AdminService(UserManager<AdminUser> adminManager,
                        SignInManager<AdminUser> signInManager,
                        AdminDbContext db)
    {
        _adminManager = adminManager;
        _signInManager = signInManager;
        _db = db;
    }

    public async Task<Result<AdminUser>> LoginAdminAsync(LoginViewModel request, CancellationToken cancellationToken)
    {
        var admin = await _adminManager.FindByEmailAsync(request.Login);
        if (admin is null) return Result.Error($"Admin with login: {request.Login} - not found");

        var loginResult = await _signInManager.PasswordSignInAsync(admin, request.Password, false, false);

        if (loginResult.Succeeded) return Result.Success(admin);
        return Result.Error("Invalid password, login pair match");
    }

    public async Task<Result<AdminUser>> GetAdminDataAsync(ClaimsPrincipal user, CancellationToken cancellationToken)
    {
        var admin = await _adminManager.GetUserAsync(user);
        if (admin is not null) return Result.Success(admin);
        return Result.Error("User is not Authorized");
    }

    public async Task<Result> LogoutAdminAsync(CancellationToken cancellationToken)
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
        if (!result.Succeeded) return Result.Error("Errors: " + string.Join(',', result.Errors));

        foreach (var role in model.Roles)
        {
            await _adminManager.AddToRoleAsync(admin, role);
        }

        return Result.Success(true);

    }

    public async Task<Result<bool>> BanAdmin(string adminId, CancellationToken cancellationToken)
    {
        var user = _adminManager.Users.FirstOrDefault(u => u.Id == adminId);
        if (user is null) return Result.Error($"Admin with ID: {adminId}, not found");
        user.Ban = true;
        await _db.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result<bool>> UnbanAdmin(string adminId, CancellationToken cancellationToken)
    {
        var user = _adminManager.Users.FirstOrDefault(u => u.Id == adminId);
        if (user is null) return Result.Error($"Admin with ID: {adminId}, not found");
        user.Ban = false;
        await _db.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}