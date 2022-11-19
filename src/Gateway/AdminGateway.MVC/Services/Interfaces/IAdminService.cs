using System.Security.Claims;
using AdminGateway.MVC.Models.DbModels;
using AdminGateway.MVC.ViewModels;
using Ardalis.Result;

namespace AdminGateway.MVC.Services.Interfaces;

public interface IAdminService
{
    Task<Result<AdminUser>> LoginAdminAsync(LoginViewModel request, CancellationToken cancellationToken);
    Task<Result<AdminUser>> GetAdminDataAsync(ClaimsPrincipal user, CancellationToken cancellationToken);
    Task<Result> LogoutAdminAsync(CancellationToken cancellationToken);
    Task<Result<bool>> CreateNewAdmin(CreateAdminVm model, CancellationToken cancellationToken);
    Task<Result<bool>> BanAdmin(string adminId, CancellationToken cancellationToken);
    Task<Result<bool>> UnbanAdmin(string adminId, CancellationToken cancellationToken);
}