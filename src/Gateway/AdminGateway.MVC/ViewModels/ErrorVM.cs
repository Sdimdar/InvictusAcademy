namespace AdminGateway.MVC.ViewModels
{
    public class ErrorVM
    {
        public string ErrorMessage { get; set; }

        public ErrorVM(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}