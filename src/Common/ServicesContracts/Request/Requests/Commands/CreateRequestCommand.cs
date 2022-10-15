using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Request.Requests.Commands;

public class CreateRequestCommand : IRequest<Result<string>>
{
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
}