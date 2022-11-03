using Ardalis.Result;
using MediatR;
using ServicesContracts.Identity.Responses;

namespace ServicesContracts.Identity.Requests.Queries;

public class GetUsersDataQuery : IRequest<Result<UsersVm>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? FilterString { get; set; }
}