using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.CloudStorage.Requests.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace CloudStorage.API.Endpoints;

public class UploadFile: EndpointBaseAsync
    .WithRequest<UploadFileCommand>
    .WithActionResult<DefaultResponseObject<string>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UploadFile(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/CloudService/UploadFile")]
    [SwaggerOperation(
        Summary = "Загрузка файла",
        Description = "Необходимо выбрать файл для загрузки на сервер",
        Tags = new[] { "S3" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<string>>> HandleAsync
    ([FromForm]UploadFileCommand file,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await _mediator.Send(file, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<string>>(response));
    }
}