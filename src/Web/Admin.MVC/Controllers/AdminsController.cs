
using Admin.MVC.Models;
using Admin.MVC.Models.DbModels;
using Admin.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



namespace Admin.MVC.Controllers;

[Authorize(Roles = "admin")]
public class AdminsController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AdminDbContext _db;

    
    public AdminsController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, AdminDbContext db)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _db = db;
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
            var user = new User
            {
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                foreach (var role in model.Roles)
                {
                    if (role == "false")
                        continue;
                    await _userManager.AddToRoleAsync(user, role);
                }
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Login", "Accounts");
            }
        
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }
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