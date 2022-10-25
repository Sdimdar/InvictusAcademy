using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Identity.Responses;

public class GetAllUsersCommand : IRequest<Result<GetAllRegisteredUsersVM>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}