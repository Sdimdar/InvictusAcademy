﻿using DataTransferLib.Models;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;

namespace UserGateway.Application.Contracts;

public interface ICoursesService
{
    Task<DefaultResponseObject<CoursesVm>?> GetCoursesAsync(GetCoursesQuery querry, CancellationToken cancellationToken);
}
