using Ardalis.Result;
using MediatR;

namespace ServicesContracts.FreeArticles.Commands;

public class EditFreeArticleCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? VideoLink { get; set; }
    public string Text { get; set; }
    public bool IsVisible { get; set; }
}