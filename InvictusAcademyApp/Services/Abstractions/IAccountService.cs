using InvictusAcademyApp.Models.RequestModels;

namespace InvictusAcademyApp.Services.Abstractions;

public interface IAccountService
{
    Task<DefaultResponse> Register(RegisterRequestModel model);
    Task<DefaultResponse> LogIn(LoginRequestModel model);
    Task LogOf();
}