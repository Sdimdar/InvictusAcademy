﻿using Ardalis.Result;
using Courses.Domain.Entities.CourseInfo;
using MediatR;

namespace ServicesContracts.Courses.Requests.Modules.Queries;

public class GetModulesByListOfIdQuery : IRequest<Result<List<ModuleInfoDbModel>?>>
{
    public IEnumerable<int> ModulesId { get; set; }
}
