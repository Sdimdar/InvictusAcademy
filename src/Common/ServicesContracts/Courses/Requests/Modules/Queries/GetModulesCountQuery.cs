using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Courses.Requests.Modules.Queries;

public class GetModulesCountQuery : IRequest<Result<int>>
{
}