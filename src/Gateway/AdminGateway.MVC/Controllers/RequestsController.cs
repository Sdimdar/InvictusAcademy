﻿using AdminGateway.MVC.Models;
using AdminGateway.MVC.Services.Interfaces;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Request.Requests.Commands;
using ServicesContracts.Request.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;

[Route("AdminPanel/[controller]/[action]")]
public class RequestsController : Controller
{
    private readonly IRequestService _requestService;
    public RequestsController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [Authorize(Roles = $"{RolesHelper.Administrator},{RolesHelper.Manager}")]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает список запросов постранично, если передать страницу 0, вернет всех",
        Description = "Необходимо передать номер страницы и количество на странице")
    ]
    public async Task<ActionResult<DefaultResponseObject<GetAllRequestVm>>> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        var response = await _requestService.GetAllRequestsAsync(pageNumber, pageSize);
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает количество записей Request , для пагинации")
    ]
    public async Task<ActionResult<DefaultResponseObject<int>>> GetRequestsCount()
    {
        var response = await _requestService.GetRequestsCountAsync();
        return Ok(response);
    }
    
    [Authorize(Roles = $"{RolesHelper.Administrator},{RolesHelper.Manager}")]
    [HttpPost]
    [SwaggerOperation(
        Summary = "Изменяет статус для Request обзвонен/необзвонен",
        Description = "Необходимо передать id")
    ]
    public async Task<ActionResult<DefaultResponseObject<string>>> ChangeCalled([FromBody] ChangeCalledStatusCommand command)
    {
        var response = await _requestService.ChangeCalledStatusAsync(command);
        return Ok(response);

    }

    [Authorize(Roles = $"{RolesHelper.Administrator},{RolesHelper.Manager}")]
    [HttpPost]
    [SwaggerOperation(
        Summary = "Добавляет комментарий",
        Description = "Необходимо передать id и комментарий")
    ]
    public async Task<ActionResult<DefaultResponseObject<string>>> ManagerComment([FromBody] ManagerCommentCommand command)
    {
        var response = await _requestService.ManagerCommentAsync(command);
        return Ok(response);
    }
}

