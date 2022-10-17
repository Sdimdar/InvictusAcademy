using System.ComponentModel.DataAnnotations;

namespace Admin.MVC.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Не указан Login")]
    public string Login { get; set; }
    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string? ReturnUrl  { get; set; }
    [Display(Name = "Запомнить?")]
    public bool RememberMe { get; set; }
}