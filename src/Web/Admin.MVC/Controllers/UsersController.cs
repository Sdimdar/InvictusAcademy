using Admin.MVC.Services.Interfaces;
using Admin.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Admin.MVC.Controllers;

public class UsersController : Controller
{
    private readonly IGetUsers _iGetUsers;

    public UsersController(IGetUsers iGetUsers)
    {
        _iGetUsers = iGetUsers;
    }
    // GET
    public IActionResult GetAllRegisteredUsers(CancellationToken cancellationToken)
    {
        var response = _iGetUsers.GetUsersAsync(cancellationToken);
        var usersList = response.Result.RegisteredUsers;
        return View(usersList);
    }
}