using CommonRepository.Models;

namespace FreeArticles.Domain.Entities;

public class FreeArticleDbModel : BaseRepositoryEntity
{
    public string Title { get; set; }
    public string? VideoLink { get; set; }
    public string Text { get; set; }
    public string ImageLink { get; set; }
    public bool IsVisible { get; set; }
}