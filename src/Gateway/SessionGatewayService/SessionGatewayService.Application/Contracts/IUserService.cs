using SessionGatewayService.Domain.Entities;

namespace SessionGatewayService.Application.Contracts;

public interface IUserService
{
    Task<UserVm> GetUserAsync(string email, CancellationToken cancellationToken);
}
