using InvictusAcademyApp.Models.RequestModels;

namespace InvictusAcademyApp.Services.Abstractions;

public interface IAccountService
{
    Task Register(RegisterRequestModel model);
    Task<DefaultResponse> LogIn(LoginRequestModel model);
    // Task LogOf();
}