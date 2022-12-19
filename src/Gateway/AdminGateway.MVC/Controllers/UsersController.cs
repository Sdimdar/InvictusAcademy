using AdminGateway.MVC.Models;
using AdminGateway.MVC.Services.Interfaces;
using AutoMapper;
using CommonStructures;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;
[Authorize]
[Route("AdminPanel/[controller]/[action]")]
public class UsersController : Controller
{
    private readonly IGetUsers _iGetUsers;
    private readonly IMapper _mapper;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IGetUsers iGetUsers, IMapper mapper, ILogger<UsersController> logger)
    {
        _iGetUsers = iGetUsers;
        _mapper = mapper;
        _logger = logger;
    }

    [Authorize(Roles = RolesHelper.Administrator)]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает список запросов постранично, если передать страницу 0, вернет всех",
        Description = "Необходимо передать номер страницы и количество на странице")
    ]
    public async Task<IActionResult> GetAllRegisteredUsers([FromQuery] int pageNumber, int pageSize)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"pageNumber {pageNumber}" + $"pageSize {pageSize}");
        var response = await _iGetUsers.GetUsersAsync(pageNumber, pageSize);
        var usersList = response.Value;
        var responce = new DefaultResponseObject<UsersVm>()
        {
            Errors = null,
            IsSuccess = true,
            ValidationErrors = null,
            Value = usersList
        };
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Users Count {response.Value.Users.Count}" +
                               $"PageNumber {response.Value.PageNumber}" +
                               $"PageSize {response.Value.PageSize}" +
                               $"");
        return Ok(responce);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает количество пользователей, для пагинации")
    ]
    public async Task<ActionResult<DefaultResponseObject<int>>> GetUsersCount()
    {
        var response = await _iGetUsers.GetUsersCount();
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" + 
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"isSucces {response.IsSuccess}" + $"Count {response.Value}" + $"");
        return Ok(response);
    }
    
    [Authorize(Roles = RolesHelper.Administrator)]
    [HttpPost]
    [SwaggerOperation(
        Summary = "Изменяет статус для User бан/не бан",
        Description = "Необходимо передать id")
    ]
    public async Task<ActionResult<DefaultResponseObject<string>>> ToBan([FromQuery] ToBanCommand command)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"Id {command.Id}");
        var response = await _iGetUsers.ChangeBanStatusAsync(command);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" + 
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"isSucces {response.IsSuccess}" +
                               $"");
        return Ok(response);
    }

}