using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Querries;
using ServicesContracts.Courses.Responses;

namespace Courses.API.Endpoints.Course
{
    public class GetCourses : EndpointBaseAsync
        .WithRequest<GetCoursesQuery>
        .WithActionResult<DefaultResponseObject<CoursesVm>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        public GetCourses(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("/Courses/GetCourses")]
        public  override async Task<ActionResult<DefaultResponseObject<CoursesVm>>> HandleAsync([FromQuery]GetCoursesQuery request,
                                                                                               CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(_mapper.Map<DefaultResponseObject<CoursesVm>>(result));
        }
    }
}
