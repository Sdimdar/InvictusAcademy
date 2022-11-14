using Microsoft.AspNetCore.Identity;

namespace AdminGateway.MVC.Models.DbModels;

public class AdminUser : IdentityUser
{
    public bool Ban { get; set; } = false;
}