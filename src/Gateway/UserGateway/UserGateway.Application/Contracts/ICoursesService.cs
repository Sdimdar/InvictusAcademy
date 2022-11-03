using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.Courses.Requests.Querries;
using ServicesContracts.Courses.Responses;

namespace UserGateway.Application.Contracts;

public interface ICoursesService:IUseExtendedHttpClient<ICoursesService>
{
    Task<DefaultResponseObject<CoursesVm>?> GetCoursesAsync(GetCoursesQuery querry, CancellationToken cancellationToken);
}
