using CommonRepository.Models;

namespace User.Domain.Entities;

public class UserDbModel : BaseRepositoryEntity
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string? InstagramLink { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public string? Citizenship { get; set; }
    public string? AvatarLink { get; set; }
    public bool IsBanned { get; set; } = false;
}