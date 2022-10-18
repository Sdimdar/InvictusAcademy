﻿
using Admin.MVC.Models.DbModels;
using Admin.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Admin.MVC.Controllers;

public class AccountsController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    public AccountsController(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public IActionResult Login(string returnUrl = null)
    {
        return View(new LoginViewModel{ReturnUrl = returnUrl});
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user = await _userManager.FindByNameAsync(model.Login);
           
            if (user == null)
            {
                ViewData["Message"] = "Такой пользователь не найден";
                return View(model);
            }
            if (user.Ban)
            {
                ViewData["Message"] = "Вы заблокированы администрацией.";
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(
                user,
                model.Password,
                model.RememberMe,
                false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
        
                return RedirectToAction("Login", "Accounts");
            }
            ModelState.AddModelError("","Неверный логин и(или) пароль");
                
        }
        
        return View(model);
        
    }
    
    public async Task<IActionResult> LogOff()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Accounts");

    }
}