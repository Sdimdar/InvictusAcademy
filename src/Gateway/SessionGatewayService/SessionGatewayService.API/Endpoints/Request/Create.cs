using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Request.Requests.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace SessionGatewayService.API.Endpoints.Request
{
    public class Create : EndpointBaseAsync
        .WithRequest<CreateRequestCommand>
        .WithActionResult<DefaultResponseObject<string>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public Create(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("Request/Create")]
        [SwaggerOperation(
            Summary = "Создание заявки пользователем",
            Description = "Для оформления заявки необходимо ввести телефон и имя",
            Tags = new[] { "Request" })
        ]
        public async override Task<ActionResult<DefaultResponseObject<string>>> HandleAsync([FromBody] CreateRequestCommand request,
                                                                                      CancellationToken cancellationToken = default)
        {
            var responce = await _mediator.Send(request, cancellationToken);
            return Ok(_mapper.Map<DefaultResponseObject<string>>(responce));
        }
    }
}
