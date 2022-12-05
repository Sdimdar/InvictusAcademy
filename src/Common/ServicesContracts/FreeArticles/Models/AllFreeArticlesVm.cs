namespace ServicesContracts.FreeArticles.Models;

public class AllFreeArticlesVm
{
    public List<FreeArticleVm> FreeArticles { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? Filter { get; set; }
}