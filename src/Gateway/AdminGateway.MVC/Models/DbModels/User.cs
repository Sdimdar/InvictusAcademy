using Microsoft.AspNetCore.Identity;

namespace AdminGateway.MVC.Models.DbModels;

public class User : IdentityUser
{
    public bool Ban { get; set; } = false;
}