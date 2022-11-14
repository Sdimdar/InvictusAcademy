using Ardalis.Result;
using MediatR;
using ServicesContracts.Identity.Responses;

namespace ServicesContracts.Identity.Requests.Queries;

public class GetAllUsersCommand : IRequest<Result<UsersVm>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? FilterString { get; set; }
}