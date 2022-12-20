using AdminGateway.MVC.Services.Interfaces;
using CommonStructures;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.FreeArticles.Commands;
using ServicesContracts.FreeArticles.Models;
using ServicesContracts.FreeArticles.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;
[Route("AdminPanel/[controller]/[action]")]

public class FreeArticlesController : Controller
{
    private readonly IFreeArticlesService _freeArticlesService;
    private readonly ILogger<FreeArticlesController> _logger;

    public FreeArticlesController(IFreeArticlesService freeArticlesService, ILogger<FreeArticlesController> logger)
    {
        _freeArticlesService = freeArticlesService;
        _logger = logger;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Создание бесплатной статьи",
        Description = "Необходимо передать в теле запроса поля"
    )]
    public async Task<ActionResult<DefaultResponseObject<string>>> Create([FromBody] CreateFreeArticleCommand request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}:" + $"Title {request.Title}" + $"Text {request.Text}" + $"ImageLink {request.ImageLink}" + $"VideoLink {request.VideoLink}");
        var response = await _freeArticlesService.Create(request);
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Редактирование бесплатной статьи",
        Description = "Необходимо передать в теле запроса поля"
    )]
    public async Task<ActionResult<DefaultResponseObject<string>>> Edit([FromBody] EditFreeArticleCommand request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}:" + $"Id {request.Id}" + $"Title {request.Title}" + $"Text {request.Text}" + $"IsVisible {request.IsVisible}" + $"ImageLink {request.ImageLink}" + $"VideoLink {request.VideoLink}");
        var response = await _freeArticlesService.Edit(request);
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение данных бесплатных статей",
        Description = "Для пагинации требуется вести в строку номер страницы, строка фильтра может быть пустой"
    )]
    public async Task<ActionResult<DefaultResponseObject<AllFreeArticlesVm>>> GetAll([FromQuery] GetAllFreeArticlesQuery request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}:" + $"FilterString {request.FilterString}" + $"PageNumber {request.PageNumber}" + $"PageSize {request.PageSize}");
        var response = await _freeArticlesService.GetAll(request);
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение данных о бесплатной статье",
        Description = "Для получения данных о пользователе необходимо передать его id через параметры в строке"
    )]
    public async Task<ActionResult<DefaultResponseObject<FreeArticleVm>>> GetFreeArticleData([FromQuery] GetFreeArticleDataQuery request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}:" + $"Id {request.Id}");
        var response = await _freeArticlesService.GetFreeArticleData(request);
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Запрос на выгрузку количества бесплатных статей",
        Description = "Не требуется ничего вводить"
    )]
    public async Task<ActionResult<DefaultResponseObject<FreeArticleVm>>> GetCount()
    {
        var response = await _freeArticlesService.GetCount();
        return Ok(response);
    }
}