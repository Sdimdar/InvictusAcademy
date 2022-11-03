using System.Security.Claims;
using AdminGateway.MVC.Models.DbModels;
using AdminGateway.MVC.ViewModels;
using Ardalis.Result;
using DataTransferLib.Models;

namespace AdminGateway.MVC.Services.Interfaces;

public interface IAdminService
{
    Task<Result<AdminUser>> LoginAdminAsync(LoginViewModel request, CancellationToken cancellationToken);
    Task<Result<AdminUser>> GetAdminDataAsync(ClaimsPrincipal user, CancellationToken cancellationToken);
    Task<Result> LogoutAdminAsync();
    
    Task<Result<bool>> CreateNewAdmin(CreateAdminVm model, CancellationToken cancellationToken);
}