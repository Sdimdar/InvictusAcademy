using InvictusAcademyApp.Enums;
using InvictusAcademyApp.Infrastructures.Databases;
using InvictusAcademyApp.Models.DbModels;
using InvictusAcademyApp.Models.RequestModels;
using InvictusAcademyApp.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InvictusAcademyApp.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly InvictusDbContext _db;

    public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, InvictusDbContext db)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _db = db;
    }

    public async Task<DefaultResponse> Register(RegisterRequestModel model)
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
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return new DefaultResponse
            {
                ResponseStatus = ResponseStatusType.Ok
            };
        }
        return new DefaultResponse
        {
            ResponseStatus = ResponseStatusType.Error
        };
    }

    public async Task<DefaultResponse> LogIn(LoginRequestModel model)
    {
        User user = await _db.Users.FirstOrDefaultAsync(u=>u.Email == model.EmailOrPhoneNumber);
        if (user is null)
        {
            user = await _db.Users.FirstOrDefaultAsync(u=>u.PhoneNumber == model.EmailOrPhoneNumber);
        }
        SignInResult result = await _signInManager.PasswordSignInAsync(
            user,
            model.Password,
            model.RememberMe,
            false
        );
        if (result.Succeeded)
            return new DefaultResponse
            {
                ResponseStatus = ResponseStatusType.Ok
            };

        return new DefaultResponse
        {
            ResponseStatus = ResponseStatusType.Error
        };
    }

    public async Task LogOf() 
        => await _signInManager.SignOutAsync();
}