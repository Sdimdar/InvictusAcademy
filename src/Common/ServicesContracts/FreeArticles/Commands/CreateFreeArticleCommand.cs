using Ardalis.Result;
using MediatR;

namespace ServicesContracts.FreeArticles.Commands;

public class CreateFreeArticleCommand : IRequest<Result<string>>
{
    public string Title { get; set; }
    public string? VideoLink { get; set; }
    public string ImageLink { get; set; }
    public string Text { get; set; }
}