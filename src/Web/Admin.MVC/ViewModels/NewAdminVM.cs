namespace Admin.MVC.ViewModels;

public class NewAdminVM
{
    public CreateAdminVM CreateModel { get; set; }
    public Dictionary<string,string> Roles { get; set; }
}