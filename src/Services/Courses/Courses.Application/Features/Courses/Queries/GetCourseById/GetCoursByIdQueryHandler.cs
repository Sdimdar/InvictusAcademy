using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using AutoMapper;
using CommonStructures;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using Courses.Domain.Entities.CourseInfo;
using MediatR;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Courses.Queries.GetCourseById;
public class GetCoursByIdQueryHandler: IRequestHandler<GetCoursByIdQuery, Result<CourseForAdminVm>>
{
    
    private readonly IMapper _mapper;
    private readonly ICourseRepository _courseRepository;

    public GetCoursByIdQueryHandler(IMapper mapper, ICourseRepository courseRepository)
    {
        _mapper = mapper;
        _courseRepository = courseRepository;
    }

    public async Task<Result<CourseForAdminVm>> Handle(GetCoursByIdQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return Result.Error("Request is null");
        }
        var result = _mapper.Map<CourseForAdminVm>(await _courseRepository.GetCourseById(request.Id));
        // var result = await _courseRepository.GetCourseById(request.Id);
        if (result is null)
        {
            return Result.NotFound();
        }
        return Result.Success(result);
    }
}
