namespace InvictusAcademyApp.Models.RequestModels;

public class LoginRequestModel
{
    // public string ReturnUrl { get; set; }
    public string EmailOrPhoneNumber { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}