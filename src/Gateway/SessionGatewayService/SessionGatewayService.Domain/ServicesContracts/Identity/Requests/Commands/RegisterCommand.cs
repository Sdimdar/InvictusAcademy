using Ardalis.Result;
using MediatR;
using SessionGatewayService.Domain.ServicesContracts.Identity.Responses;
using System.Security.Claims;

namespace SessionGatewayService.Domain.ServicesContracts.Identity.Requests.Commands;

public class RegisterCommand : IRequest<Result<RegisterVm>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string? InstagramLink { get; set; }
    public string? Citizenship { get; set; }

}
