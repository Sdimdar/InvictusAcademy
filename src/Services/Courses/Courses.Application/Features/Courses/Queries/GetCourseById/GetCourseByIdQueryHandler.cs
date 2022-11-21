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
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Courses.Queries.GetCourseById;
public class GetCourseByIdQueryHandler: IRequestHandler<GetCoursByIdQuery, Result<CourseForAdminVm>>
{
    
    private readonly IMapper _mapper;
    private readonly ICourseRepository _courseRepository;
    private readonly ILogger<GetCourseByIdQueryHandler> _logger;

    public GetCourseByIdQueryHandler(IMapper mapper, ICourseRepository courseRepository, ILogger<GetCourseByIdQueryHandler> logger)
    {
        _mapper = mapper;
        _courseRepository = courseRepository;
        _logger = logger;
    }

    public async Task<Result<CourseForAdminVm>> Handle(GetCoursByIdQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Request is null");
            return Result.Error($"{BussinesErrors.NotFound.ToString()}: Request is null");
        }
        var result = _mapper.Map<CourseForAdminVm>(await _courseRepository.GetCourseById(request.Id));
        if (result is null)
        {
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Course with ID: {request.Id} not found");
            return Result.Error($"{BussinesErrors.NotFound.ToString()}: Course with ID: {request.Id} not found");
        }
        return Result.Success(result);
    }
}
