using Ardalis.Result;
using AutoMapper;
using CloudStorage.Application.Contracts;
using CommonStructures;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ServicesContracts.CloudStorage.Requests.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;

public class CloudStorageController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICloudStorageRepository _cloudStorage;

    public CloudStorageController(IMediator mediator, IMapper mapper, ICloudStorageRepository cloudStorage)
    {
        _mediator = mediator;
        _mapper = mapper;
        _cloudStorage = cloudStorage;
    }
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)] 
    [DisableRequestSizeLimit]
    [HttpPost("/CloudStorage/UploadFile")]
    [SwaggerOperation(
        Summary = "Загрузка файла",
        Description = "Необходимо выбрать файл для загрузки на сервер",
        Tags = new[] { "S3" })
    ]
    public async Task<ActionResult<DefaultResponseObject<string>>> UploadFile(CancellationToken cancellationToken)
    {
        IFormFileCollection formFileCollection = Request.Form.Files;
        IFormFile formFile = formFileCollection![0];
        var sizeLimit = formFile.Length;
        if (sizeLimit < 1073741824)//1 GB
        {
            var fileAlreadyExist = await _cloudStorage.GetFilesByName(formFile.FileName);
            if (!fileAlreadyExist)
            {
                var allowedFileExtension = new List<string>(){".mp4", ".mov", ".jpg", ".jpeg",".png",".pdf",".doc",".docx", ".zip",".7z",".rar"};
                var fileExt = System.IO.Path.GetExtension(formFile.FileName);
                if (allowedFileExtension.Any(a => a.Equals(fileExt)))
                {
                    string filePath = String.Empty;
                    if (formFile.Length > 0) {
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), formFile.FileName);
                        await using Stream fileStream = new FileStream(filePath, FileMode.Create);
                        await formFile.CopyToAsync(fileStream, cancellationToken);
                    }
        
                    var response = await _mediator.Send(new UploadFileCommand{FilePath = filePath} , cancellationToken);
                    return Ok(_mapper.Map<DefaultResponseObject<string>>(response));
                }
            }
        }
        var eResult = await _mediator.Send(new UploadFileCommand{FilePath = string.Empty} , cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<string>>(eResult));
    }

    [HttpPost("/CloudStorage/GetFilePathByName")]
    [SwaggerOperation(
        Summary = "Поиск файла по имени",
        Description = "Впишите имя файла для поиска",
        Tags = new[] {"S3"})
    ]
    public async Task<ActionResult<DefaultResponseObject<List<string>>>> GetFilePathByName([FromQuery]string fileName, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(new GetFilePathByName() {FileName = fileName},
            cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<List<string>>>(result));
    }
}