namespace AdminGateway.MVC.ViewModels
{
    public class NewAdminVm
    {
        public CreateAdminVm CreateModel { get; set; }
        public Dictionary<string, string> Roles { get; set; }
    }
}