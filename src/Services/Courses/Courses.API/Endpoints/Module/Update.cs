using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Modules.Commands;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Module
{
    public class Update : EndpointBaseAsync
        .WithRequest<UpdateModuleCommand>
        .WithActionResult<DefaultResponseObject<ModuleInfoVm>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public Update(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("/Modules/Update")]
        [SwaggerOperation(
            Summary = "Изменение данных о модуле",
            Description = "В теле запроса передаются новые данные для модуля",
            Tags = new[] { "Module" })
        ]
        public override async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> HandleAsync([FromBody] UpdateModuleCommand request,
                                                                                                  CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(_mapper.Map<DefaultResponseObject<ModuleInfoVm>>(result));
        }
    }
}
