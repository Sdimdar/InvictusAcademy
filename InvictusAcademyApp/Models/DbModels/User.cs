using Microsoft.AspNetCore.Identity;

namespace InvictusAcademyApp.Models.DbModels;

public class User : IdentityUser
{
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string? InstagramLink { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string Citizenship { get; set; }
}