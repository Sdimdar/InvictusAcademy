using AdminGateway.MVC.Models;
using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using CommonStructures;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Modules.Commands;
using ServicesContracts.Courses.Requests.Modules.Queries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;
[Authorize(Roles = $"{RolesHelper.Administrator},{RolesHelper.Instructor}")]
[Route("AdminPanel/[controller]/[action]")]
public class ModulesController : Controller
{
    private readonly IModulesService _modulesService;
    private readonly ILogger<ModulesController> _logger;

    public ModulesController(IModulesService modulesService, ILogger<ModulesController> logger)
    {
        _modulesService = modulesService;
        _logger = logger;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Добавление статей в модуль",
        Description = "Необходимо передать в теле запроса объект с ID модуля и список объектов статей"
    )]
    public async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> AddArticles([FromBody] AddArticlesCommand request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"Articles Count {request.Articles.Count}" + $"ModuleId {request.ModuleId}");
        var response = await _modulesService.AddArticle(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Id {response.Value.Id}" +
                               $"Title {response.Value.Title}" +
                               $"ShortDescription {response.Value.ShortDescription}" +
                               $"Articles Count {response.Value.Articles.Count}" +
                               $"");
        return Ok(response);
    }
    [HttpPost]
    [SwaggerOperation(
        Summary = "Добавление теста в статью",
        Description = "Необходимо передать в теле запроса объект с ID модуля, порядковый номер статьи и тест"
    )]
    public async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> AddTest([FromBody] AddTestCommand request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"TestCompleteCount {request.Test.TestCompleteCount}" + $"TestShowCount {request.Test.TestShowCount}" + $"ModuleId {request.ModuleId}");
        var response = await _modulesService.AddTest(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Id {response.Value.Id}" +
                               $"Title {response.Value.Title}" +
                               $"ShortDescription {response.Value.ShortDescription}" +
                               $"Articles Count {response.Value.Articles.Count}" +
                               $"");
        return Ok(response);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Создание модуля",
        Description = "Для создания модуля нужно передать его название и описание, также можно сразу передать вместе с статьями"
    )]
    public async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> Create([FromBody] CreateModuleCommand request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"Title {request.Title}" + $"ShortDescription {request.ShortDescription}");
        var response = await _modulesService.Create(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Id {response.Value.Id}" +
                               $"Title {response.Value.Title}" +
                               $"ShortDescription {response.Value.ShortDescription}" +
                               $"Articles Count {response.Value.Articles.Count}" +
                               $"");
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Удаление модуля по его ID",
        Description = "Необходимо передать в теле метода объект содержащий ID метода на удаление, " +
                      "если этот метод используется в каком либо курсе вернёт ошибку"
    )]
    public async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> Delete([FromBody] DeleteModuleCommand request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"Id {request.Id}");
        var response = await _modulesService.Delete(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Id {response.Value.Id}" +
                               $"Title {response.Value.Title}" +
                               $"ShortDescription {response.Value.ShortDescription}" +
                               $"Articles Count {response.Value.Articles.Count}" +
                               $"");
        return Ok(response);
    }

    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Изменение данных о модуле",
        Description = "В теле запроса передаются новые данные для модуля"
    )]
    public async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> Update([FromBody] UpdateModuleCommand request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" +
                               $"Id {request.Id}" +
                               $"Title {request.Title}" +
                               $"ShortDescription {request.ShortDescription}" +
                               $"Articles Count {request.Articles.Count}");
        var response = await _modulesService.Update(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Id {response.Value.Id}" +
                               $"Title {response.Value.Title}" +
                               $"ShortDescription {response.Value.ShortDescription}" +
                               $"Articles Count {response.Value.Articles.Count}" +
                               $"");
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение данных о всех модулях",
        Description = "Получение данных о всех модулях в базе данных"
    )]
    public async Task<ActionResult<DefaultResponseObject<List<ModuleInfoVm>>>> GetAll()
    {
        var response = await _modulesService.GetAll();
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Count {response.Value.Count}" +
                               $"");
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает количество модулей, для пагинации")
    ]
    public async Task<ActionResult<DefaultResponseObject<int>>> GetModulesCount()
    {
        var response = await _modulesService.GetModulesCount();
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" + 
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"isSucces {response.IsSuccess}" + $"Count {response.Value}" + $"");
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение данных о модулях которые подходят под строку фильтрации",
        Description = "Необходимо передать в строке строку фильтрации"
    )]
    public async Task<ActionResult<DefaultResponseObject<List<ModuleInfoVm>>>> GetByFilterString(GetModulesByFilterStringQuery request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" +
                               $"FilterString {request.FilterString}");
        var response = await _modulesService.GetFilterByString(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Count {response.Value.Count}" +
                               $"");
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение данных о модуле по его ID",
        Description = "Необходимо передать в строке запроса Id модуля"
    )]
    public async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> GetById([FromQuery] ModuleByIdVm request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"Id {request.Id}");
        var response = await _modulesService.GetById(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Id {response.Value.Id}" +
                               $"Title {response.Value.Title}" +
                               $"ShortDescription {response.Value.ShortDescription}" +
                               $"Articles Count {response.Value.Articles.Count}" +
                               $"");
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение данных о модуле по его ID",
        Description = "Необходимо передать в строке запроса Id модуля"
    )]
    public async Task<ActionResult<DefaultResponseObject<List<ModuleInfoVm>>>> GetByListOfId([FromQuery] GetModulesByListOfIdQuery request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"ModulesId {request.ModulesId}");
        var response = await _modulesService.GetByListOfId(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Count {response.Value.Count}" +
                               $"");
        return Ok(response);
    }
}