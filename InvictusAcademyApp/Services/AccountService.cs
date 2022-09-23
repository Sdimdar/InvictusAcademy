using InvictusAcademyApp.Models.DbModels;
using InvictusAcademyApp.Models.RequestModels;
using InvictusAcademyApp.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace InvictusAcademyApp.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task Register(RegisterRequestModel model)
    {
        User user = new User
        {
            UserName = model.Email,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            FirstName = model.FirstName,
            MiddleName = model.MiddleName,
            LastName = model.LastName,
            InstagramLink = model.InstagramLink,
            RegistrationDate = DateTime.Today.Date,
            Citizenship = model.Citizenship
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        // if (result.Succeeded)
        // {
        //     await _signInManager.SignInAsync(user, false);
        // }
    }
}