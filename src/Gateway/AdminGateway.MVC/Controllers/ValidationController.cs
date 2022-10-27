using AdminGateway.MVC.Models.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminGateway.MVC.Controllers;

public class ValidationController : Controller
{
    private readonly UserManager<AdminUser> _userManager;

    public ValidationController(UserManager<AdminUser> userManager)
    {

        _userManager = userManager;
    }


    public bool CheckUserName(string userName)
    {
        var checkName = _userManager.Users.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
        if (checkName != null)
            return false;
        return true;
    }


}