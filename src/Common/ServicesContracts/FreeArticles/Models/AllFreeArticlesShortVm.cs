namespace ServicesContracts.FreeArticles.Models;

public class AllFreeArticlesShortVm
{
    public List<FreeArticleShortVm> FreeArticles { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? Filter { get; set; }
}
