using FluentValidation;
using ServicesContracts.CloudStorage.Requests.Querries;

namespace CloudStorage.Application.Features.Queries.GetAllFiles;

public class GetAllFilesQueryValidator : AbstractValidator<GetAllFilesQuery>
{
    public GetAllFilesQueryValidator()
    {
        RuleFor(p => p.PageNumber)
            .GreaterThan(0).WithMessage("Page number can't be less then 1");
        RuleFor(p => p.PageSize)
            .GreaterThan(-1).WithMessage("Page size can't be less then 0");
        
    }
}