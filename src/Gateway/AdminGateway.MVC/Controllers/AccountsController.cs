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
        Description = "Для входа админа необходимо ввести логин и пароль")
    ]
    public async Task<ActionResult<DefaultResponseObject<AdminUser>>> Login([FromBody] LoginViewModel request,
                                                                            CancellationToken cancellationToken)
    {
        var response = await _adminService.LoginAdminAsync(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<AdminUser>>(response));
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение данных о пользователе",
        Description = "Для получения данных пользователь должен быть залогинен")
    ]
    public async Task<ActionResult<DefaultResponseObject<AdminUser>>> GetAdminData(CancellationToken cancellationToken)
    {
        var admin = await _adminService.GetAdminDataAsync(User, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<AdminUser>>(admin));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Логаут текущего залогиненного админа",
        Description = "Администратор должен быть уже залогинен")
    ]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        await _adminService.LogoutAdminAsync(cancellationToken);
        return Ok();
    }
}