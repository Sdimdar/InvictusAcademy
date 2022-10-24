using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AdminGateway.MVC.ViewModels;

public class CreateAdminVm
{
    [Required(ErrorMessage = "Укажите логин")]
    [Remote(action: "CheckUserName", controller: "Validation", ErrorMessage = "Такой логин уже существует")]
    [StringLength(40, MinimumLength = 4, ErrorMessage = "Минимальное количество знаков должно быть больше 4")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Введите пароль")]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$",
        ErrorMessage = "Минимум восемь символов, как минимум одна заглавная английская буква, одна строчная английская буква, одна цифра и один специальный символ")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; }
    public List<string> Roles { get; set; }
}