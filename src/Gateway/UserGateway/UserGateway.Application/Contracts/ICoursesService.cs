using DataTransferLib.Models;
using ServicesContracts.Courses.Requests.Querries;
using ServicesContracts.Courses.Responses;

namespace UserGateway.Application.Contracts;

public interface ICoursesService
{
    Task<DefaultResponseObject<CoursesVm>?> GetCoursesAsync(GetCoursesQuerry querry, CancellationToken cancellationToken);
}
