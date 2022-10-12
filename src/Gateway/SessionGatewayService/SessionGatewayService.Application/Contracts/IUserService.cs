using Ardalis.Result;
using DataTransferLib.Models;
using SessionGatewayService.Domain.Entities;

namespace SessionGatewayService.Application.Contracts;

public interface IUserService
{
    Task<DefaultResponceObject<UserVm>> GetUserAsync(string email, CancellationToken cancellationToken);
}
