using AdminGateway.MVC.Models.DbModels;
using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using AutoMapper;
using CommonStructures;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;

[Route("AdminPanel/[controller]/[action]")]
public class AccountsController : Controller
{
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;
    private readonly ILogger<AccountsController> _logger;

    public AccountsController(IAdminService adminService, IMapper mapper, ILogger<AccountsController> logger)
    {
        _adminService = adminService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Вход админа в систему",
        Description = "Для входа админа необходимо ввести логин и пароль")
    ]
    public async Task<ActionResult<DefaultResponseObject<AdminUser>>> Login([FromBody] LoginViewModel request,
                                                                            CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}: " +
                               $"Login: {request.Login}");
        var response = await _adminService.LoginAdminAsync(request, cancellationToken);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"IsSucces: {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Email: {response.Value.Email}" +
                               $"Ban: {response.Value.Ban}");
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
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"Email: {admin.Value.Email}" +
                               $"Ban: {admin.Value.Ban}");
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