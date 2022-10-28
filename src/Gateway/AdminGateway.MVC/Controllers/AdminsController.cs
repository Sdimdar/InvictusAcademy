using AdminGateway.MVC.Models;
using AdminGateway.MVC.Models.DbModels;
using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminGateway.MVC.Controllers;

[Authorize(Roles = "admin")]
public class AdminsController : Controller
{
    private readonly UserManager<AdminUser> _userManager;
    private readonly AdminDbContext _db;
    private readonly IAdminCreate _adminCreate;


    public AdminsController(UserManager<AdminUser> userManager, AdminDbContext db, IAdminCreate adminCreate)
    {
        _userManager = userManager;
        _db = db;
        _adminCreate = adminCreate;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAdmin(CreateAdminVm model)
    {
        if (ModelState.IsValid)
        {
            if (await _adminCreate.CreateNewAdmin(model))
                return RedirectToAction("EditProfile");
        }
        ViewData["Error"] = "Произошла ошибка создания админа.";
        return RedirectToAction("CreateAdmin");

    }

    [HttpGet]
    public IActionResult EditProfile()
    {
        var users = _userManager.Users.ToList();

        users.Remove(users[0]);
        return Ok();
    }

    [HttpPost]
    public IActionResult EditProfile([FromBody] UserIdVm model)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.Id == model.UserId);
        if (user is not null)
        {
            if (user.Ban)
                user.Ban = false;
            else
                user.Ban = true;
            _db.SaveChanges();
            return Ok(user.Ban);
        }
        return BadRequest();

    }

}