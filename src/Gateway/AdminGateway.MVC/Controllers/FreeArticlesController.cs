using AdminGateway.MVC.Services.Interfaces;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Responses;
using ServicesContracts.FreeArticles.Commands;
using ServicesContracts.FreeArticles.Models;
using ServicesContracts.FreeArticles.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;
[Route("AdminPanel/[controller]/[action]")]

public class FreeArticlesController : Controller
{
    private readonly IFreeArticlesService _freeArticlesService;

    public FreeArticlesController(IFreeArticlesService freeArticlesService)
    {
        _freeArticlesService = freeArticlesService;
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Получение всех бесплатных статей",
        Description = "Необходимо передать в теле запроса поля"
    )]
    public async Task<ActionResult<DefaultResponseObject<string>>> Create([FromBody]CreateFreeArticleCommand request)
    {
        var response = await _freeArticlesService.Create(request);
        return Ok(response);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Редактирование бесплатной статьи",
        Description = "Необходимо передать в теле запроса поля"
    )]
    public async Task<ActionResult<DefaultResponseObject<string>>> Edit([FromBody]EditFreeArticleCommand request)
    {
        var response = await _freeArticlesService.Edit(request);
        return Ok(response);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение данных бесплатных статей",
        Description = "Для пагинации требуется вести в строку номер страницы, строка фильтра может быть пустой"
    )]
    public async Task<ActionResult<DefaultResponseObject<AllFreeArticlesVm>>> GetAll([FromQuery]GetAllFreeArticlesQuery request)
    {
        var response = await _freeArticlesService.GetAll(request);
        return Ok(response);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение данных о бесплатной статье",
        Description = "Для получения данных о пользователе необходимо передать его id через параметры в строке"
    )]
    public async Task<ActionResult<DefaultResponseObject<FreeArticleVm>>> GetFreeArticleData([FromQuery]GetFreeArticleDataQuery request)
    {
        var response = await _freeArticlesService.GetFreeArticleData(request);
        return Ok(response);
    }
}