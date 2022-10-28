using AdminGateway.MVC.Models.DbModels;
using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using AutoMapper;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;
[Route("AdminPanel/[controller]/[action]")]
public class AccountsController : Controller
{
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;

    public AccountsController(IAdminService adminService, IMapper mapper)
    {
        _adminService = adminService;
        _mapper = mapper;
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Вход админа в систему",
        Description = "Для входа админа необходимо ввести логин и пароль",
        Tags = new[] { "Admin" })
    ]
    public async Task<IActionResult> Login([FromBody]LoginViewModel request, 
        CancellationToken cancellationToken = default)
    { 
        try
        {
            var response = await _adminService.LoginAdminAsync(request, cancellationToken);
            return Ok(_mapper.Map<DefaultResponseObject<AdminUser>>(response));
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение данных о пользователе",
        Description = "Для получения данных пользователь должен быть залогинен",
        Tags = new[] { "Admin" })
    ]
    public async Task<IActionResult> GetAdminData(CancellationToken cancellationToken = default)
    {
        var admin = await _adminService.GetAdminDataAsync(User, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<AdminUser>>(admin));
    }

    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        var response = await _adminService.LogoutAdminAsync();
        return Ok(response);
    }
}