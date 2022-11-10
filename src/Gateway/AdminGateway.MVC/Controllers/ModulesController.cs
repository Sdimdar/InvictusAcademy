using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using CommonStructures;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Requests.Modules.Commands;
using ServicesContracts.Courses.Requests.Modules.Queries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;
[Route("AdminPanel/[controller]/[action]")]
public class ModulesController: Controller
{
    private readonly IModulesService _modulesService;

    public ModulesController(IModulesService modulesService)
    {
        _modulesService = modulesService;
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Добавление статей в модуль",
        Description = "Необходимо передать в теле запроса объект с ID модуля и список объектов статей"
    )]
    public async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> AddArticles([FromBody]AddArticlesCommand request)
    {
        try
        {
            var response = await _modulesService.AddArticle(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Создание модуля",
        Description = "Для создания модуля нужно передать его название и описание, также можно сразу передать вместе с статьями"
    )]
    public async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> Create([FromBody]CreateModuleCommand request)
    {
        try
        {
            var response = await _modulesService.Create(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Удаление модуля по его ID",
        Description = "Необходимо передать в теле метода объект содержащий ID метода на удаление, " +
                      "если этот метод используется в каком либо курсе вернёт ошибку"
    )]
    public async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> Delete([FromBody]DeleteModuleCommand request)
    {
        try
        {
            var response = await _modulesService.Delete(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Изменение данных о модуле",
        Description = "В теле запроса передаются новые данные для модуля"
    )]
    public async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> Update([FromBody]UpdateModuleCommand request)
    {
        try
        {
            var response = await _modulesService.Update(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение данных о всех модулях",
        Description = "Получение данных о всех модулях в базе данных"
    )]
    public async Task<ActionResult<DefaultResponseObject<List<ModuleInfoVm>>>> GetAll()
    {
        try
        {
            var response = await _modulesService.GetAll();
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение данных о модулях которые подходят под строку фильтрации",
        Description = "Необходимо передать в строке строку фильтрации"
    )]
    public async Task<ActionResult<DefaultResponseObject<List<ModuleInfoVm>>>> GetByFilterString([FromQuery]GetModulesByFilterStringQuery request)
    {
        try
        {
            var response = await _modulesService.GetFilterByString(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение данных о модуле по его ID",
        Description = "Необходимо передать в строке запроса Id модуля"
    )]
    public async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> GetById([FromQuery]GetModuleByIdQuery request)
    {
        try
        {
            var response = await _modulesService.GetById(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение данных о модуле по его ID",
        Description = "Необходимо передать в строке запроса Id модуля"
    )]
    public async Task<ActionResult<DefaultResponseObject<List<ModuleInfoVm>>>> GetByListOfId([FromQuery]GetModulesByListOfIdQuery request)
    {
        try
        {
            var response = await _modulesService.GetByListOfId(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
}