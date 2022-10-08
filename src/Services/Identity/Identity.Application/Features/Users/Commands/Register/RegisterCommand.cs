using Ardalis.Result;
using MediatR;
using System.Security.Claims;

namespace Identity.Application.Features.Users.Commands.Register;

public class RegisterCommand : IRequest<(List<Claim>?, Result<RegisterCommandVm>)>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string? InstagramLink { get; set; }
    public string Citizenship { get; set; }

    public RegisterCommand(string email,
                          string password,
                          string passwordConfirm,
                          string phoneNumber,
                          string firstName,
                          string? middleName,
                          string lastName,
                          string? instagramLink,
                          string citizenship)
    {
        Email = email;
        Password = password;
        PasswordConfirm = passwordConfirm;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        InstagramLink = instagramLink;
        Citizenship = citizenship;
    }
}
