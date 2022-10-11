using Admin.MVC.Models;
using Admin.MVC.Models.DbModels;
using Admin.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Admin.MVC.Controllers;
[Authorize(Roles = "admin")]
public class AdminsController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    

    public AdminsController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        
    }
    
    [HttpGet]
    public IActionResult CreateAdmin() => View();

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
                    await _userManager.AddToRoleAsync(user, role);
                }
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Login", "Accounts");
            }
        
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }
        return View(model);
        
    }
}