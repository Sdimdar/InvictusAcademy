namespace AdminGateway.MVC.ViewModels;

public class CreateAdminVm
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public string ConfirmPassword { get; set; }
    public List<string> Roles { get; set; }
}