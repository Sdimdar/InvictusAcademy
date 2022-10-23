using DataTransferLib.Models;
using ServicesContracts.Courses.Requests.Querries;
using ServicesContracts.Courses.Responses;

namespace SessionGatewayService.Application.Contracts;

public interface ICoursesService
{
    Task<DefaultResponseObject<CoursesVm>?> GetCoursesAsync(GetCoursesQuerry querry, CancellationToken cancellationToken);
}
