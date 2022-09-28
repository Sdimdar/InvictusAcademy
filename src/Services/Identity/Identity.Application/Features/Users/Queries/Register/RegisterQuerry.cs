using Ardalis.Result;
using MediatR;

namespace Identity.Application.Features.Users.Queries.Register;

public class RegisterQuerry : IRequest<Result<string>>
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

    public RegisterQuerry(string email,
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
