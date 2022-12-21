using AdminGateway.MVC.Services.Interfaces;
using CommonStructures;
using CloudStorage.Domain.Entities;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.CloudStorage.Requests.Commands;
using ServicesContracts.CloudStorage.Requests.Queries;
using ServicesContracts.CloudStorage.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;
[Route("AdminPanel/[controller]/[action]")]
public class CloudStorageController : Controller
{
    private readonly ICloudStorages _cloudStorages;
    private readonly ILogger<CloudStorageController> _logger;

    public CloudStorageController(ICloudStorages cloudStorages, ILogger<CloudStorageController> logger)
    {
        _cloudStorages = cloudStorages;
        _logger = logger;
    }
    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает список запросов постранично, если передать страницу 0, вернет всех",
        Description = "Необходимо передать номер страницы и количество на странице")
    ]
    public async Task<ActionResult<DefaultResponseObject<GetAllFilesVM>>> GetAllFiles(int pageNumber, int pageSize, string? filterString)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}: " + $"pageNumbe" + $"r: {pageNumber}" + $"pageSize: {pageSize}");
        var response = await _cloudStorages.GetFilesAsync(pageNumber, pageSize, filterString);
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает количество записей Request , для пагинации")
    ]
    public async Task<ActionResult<DefaultResponseObject<int>>> GetFilesCount()
    {
        var response = await _cloudStorages.GetFilesCount();
        return Ok(response);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение данных о модулях которые подходят под строку фильтрации",
        Description = "Необходимо передать в строке строку фильтрации"
    )]
    public async Task<ActionResult<DefaultResponseObject<List<GetAllFilesVM>>>> GetFilterByString(GetFilesByFilterStringQuery request)
    {
        var response = await _cloudStorages.GetFilterByString(request);
        return Ok(response);
    }
    
}