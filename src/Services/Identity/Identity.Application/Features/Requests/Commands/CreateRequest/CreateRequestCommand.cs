using Ardalis.Result;
using MediatR;

namespace Identity.Application.Features.Requests.Commands.CreateRequest;

public class CreateRequestCommand:IRequest<Result>
{
    public CreateRequestCommand(string userName, string phoneNumber)
    {
        UserName = userName;
        PhoneNumber = phoneNumber;
    }

    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
}