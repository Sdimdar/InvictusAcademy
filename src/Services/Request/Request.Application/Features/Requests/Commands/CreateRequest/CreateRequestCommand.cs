using Ardalis.Result;
using MediatR;

namespace Request.Application.Features.Requests.Commands.CreateRequest;

public class CreateRequestCommand : IRequest<Result>
{
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
}