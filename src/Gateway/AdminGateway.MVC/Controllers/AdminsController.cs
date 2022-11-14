using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using AutoMapper;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;
[Route("AdminPanel/[controller]/[action]")]
public class AdminsController : Controller
{
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;


    public AdminsController(IAdminService adminService, IMapper mapper)
    {
        _adminService = adminService;
        _mapper = mapper;
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Создание нового администратора",
        Description = "Для создания нового администратора необходимо передать логин, пароль, подтверждение пароля и его роль")
    ]
    public async Task<ActionResult<DefaultResponseObject<bool>>> CreateAdmin([FromBody] CreateAdminVm request, 
                                                                             CancellationToken cancellationToken)
    {
        var response = await _adminService.CreateNewAdmin(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<bool>>(response));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Выдача бана админу по его ID",
        Description = "В теле передаётся ID админа, ему будет выдан бан, если он был в бане, то его разбанит")
    ]
    public async Task<ActionResult<DefaultResponseObject<bool>>> BanAdmin([FromBody] UserIdVm request, 
                                                                          CancellationToken cancellationToken)
    {
        var response = await _adminService.BanAdmin(request.UserId, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<bool>>(response));
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Снятие бана с админа по его ID",
        Description = "В теле передаётся ID админа, ему будет выдан бан, если он был в бане, то его разбанит")
    ]
    public async Task<ActionResult<DefaultResponseObject<bool>>> UnbanAdmin([FromBody] UserIdVm request, 
                                                                            CancellationToken cancellationToken)
    {
        var response = await _adminService.UnbanAdmin(request.UserId, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<bool>>(response));
    }
}