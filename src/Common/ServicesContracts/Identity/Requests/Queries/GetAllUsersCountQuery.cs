using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Identity.Requests.Queries;

public class GetAllUsersCountQuery : IRequest<Result<int>>
{

}