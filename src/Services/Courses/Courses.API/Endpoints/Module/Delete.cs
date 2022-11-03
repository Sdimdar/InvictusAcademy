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
    public class Delete : EndpointBaseAsync
        .WithRequest<DeleteModuleCommand>
        .WithActionResult<DefaultResponseObject<ModuleInfoVm>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public Delete(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("/Modules/Delete")]
        [SwaggerOperation(
            Summary = "Удаление модуля по его ID",
            Description = "Необходимо передать в теле метода объект содержащий ID метода на удаление, " +
                          "если этот метод используется в каком либо курсе вернёт ошибку",
            Tags = new[] { "Module" })
        ]
        public async override Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> HandleAsync([FromBody] DeleteModuleCommand request,
                                                                                                  CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(_mapper.Map<DefaultResponseObject<ModuleInfoVm>>(result));
        }
    }
}
