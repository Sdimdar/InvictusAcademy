using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Identity.Responses;

public class GetAllUsersCommand : IRequest<Result<UsersVm>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? FilterString { get; set; }  
}