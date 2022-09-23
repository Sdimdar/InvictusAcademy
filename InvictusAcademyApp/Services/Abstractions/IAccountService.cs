using InvictusAcademyApp.Models.RequestModels;

namespace InvictusAcademyApp.Services.Abstractions;

public interface IAccountService
{
    Task Register(RegisterRequestModel model);
    // Task LogIn(LoginRequestModel model);
    // Task LogOf();
}