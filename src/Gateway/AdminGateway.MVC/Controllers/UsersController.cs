using AdminGateway.MVC.Services.Interfaces;
using AutoMapper;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;

[Route("AdminPanel/[controller]/[action]")]
public class UsersController : Controller
{
    private readonly IGetUsers _iGetUsers;
    private readonly IMapper _mapper;

    public UsersController(IGetUsers iGetUsers, IMapper mapper)
    {
        _iGetUsers = iGetUsers;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает список запросов постранично, если передать страницу 0, вернет всех",
        Description = "Необходимо передать номер страницы и количество на странице")
    ]
    public async Task<IActionResult> GetAllRegisteredUsers([FromQuery] int pageNumber, int pageSize)
    {
        var response = await _iGetUsers.GetUsersAsync(pageNumber, pageSize);
        var usersList = response.Value;
        var responce = new DefaultResponseObject<UsersVm>()
        {
            Errors = null,
            IsSuccess = true,
            ValidationErrors = null,
            Value = usersList
        };
        return Ok(responce);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает количество пользователей, для пагинации")
    ]
    public async Task<ActionResult<DefaultResponseObject<int>>> GetUsersCount()
    {
        var response = await _iGetUsers.GetUsersCount();
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Изменяет статус для User бан/не бан",
        Description = "Необходимо передать id")
    ]
    public async Task<ActionResult<DefaultResponseObject<string>>> ToBan([FromQuery] ToBanCommand command)
    {
        var response = await _iGetUsers.ChangeBanStatusAsync(command);
        return Ok(response);
    }

}