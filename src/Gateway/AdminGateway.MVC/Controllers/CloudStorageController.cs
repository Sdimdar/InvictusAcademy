using AdminGateway.MVC.Services.Interfaces;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.CloudStorage.Requests.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;
[Route("AdminPanel/[controller]/[action]")]
public class CloudStorageController : Controller
{
    private readonly ICloudStorages _cloudStorages;

    public CloudStorageController(ICloudStorages cloudStorages)
    {
        _cloudStorages = cloudStorages;
    }
    [HttpPost]
    [SwaggerOperation(
        Summary = "Загрузка файла на облако",
        Description = "Выберите файл для загрузки")
    ]
    public async Task<ActionResult<DefaultResponseObject<string>>> UploadFile([FromForm]UploadFileCommand fileCommand)
    {
        var response = await _cloudStorages.Upload(fileCommand);
        return Ok(response);

    }
}