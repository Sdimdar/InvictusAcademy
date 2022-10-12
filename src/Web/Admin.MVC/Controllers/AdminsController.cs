
using Admin.MVC.Models.DbModels;
using Admin.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Admin.MVC.Controllers;

public class AdminsController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;


    public AdminsController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
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
}