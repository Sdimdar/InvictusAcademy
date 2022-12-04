namespace ServicesContracts.Identity.Responses;

public class RegisteredUserVM
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Citizenship { get; set; }
    public string City { get; set; }
    public DateTime RegistrationDate { get; set; }
}