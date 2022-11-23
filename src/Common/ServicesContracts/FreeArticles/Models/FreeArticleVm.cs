namespace ServicesContracts.FreeArticles.Models;

public class FreeArticleVm
{
    public string Title { get; set; }
    public string? VideoLink { get; set; }
    public string Text { get; set; }
    public bool IsVisible { get; set; }
}