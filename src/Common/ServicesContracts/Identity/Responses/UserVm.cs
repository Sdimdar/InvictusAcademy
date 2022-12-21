namespace ServicesContracts.Identity.Responses;

public class UserVm
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string? InstagramLink { get; set; }
    public string Citizenship { get; set; }
    public string City { get; set; }
    public string RegistrationDate { get; set; }
}
