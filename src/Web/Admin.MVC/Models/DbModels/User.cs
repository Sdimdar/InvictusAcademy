using Microsoft.AspNetCore.Identity;

namespace Admin.MVC.Models.DbModels;

public class User : IdentityUser
{
    public bool Ban { get; set; } = false;
}