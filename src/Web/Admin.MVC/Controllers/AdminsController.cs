
using Admin.MVC.Models;
using Admin.MVC.Models.DbModels;
using Admin.MVC.Services.Interfaces;
using Admin.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



namespace Admin.MVC.Controllers;

[Authorize(Roles = "admin")]
public class AdminsController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly AdminDbContext _db;
    private readonly IAdminCreate _adminCreate;

    
    public AdminsController(UserManager<User> userManager,  AdminDbContext db, IAdminCreate adminCreate)
    {
        _userManager = userManager;
        _db = db;
        _adminCreate = adminCreate;
    }

    [HttpGet]
    public IActionResult CreateAdmin()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdmin(CreateAdminVM model)
    {
        if (ModelState.IsValid)
        {
            if(await _adminCreate.CreateNewAdmin(model))
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
        return View(new EditProfileVm
        {
            Users = users
        });
    }

    [HttpPost]
    public IActionResult EditProfile([FromBody]UserIdVm model)
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