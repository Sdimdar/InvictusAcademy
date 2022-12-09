namespace ServicesContracts.Courses.Responses;

public class PurchasedModuleInfoVm
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public int ArticlesCount { get; set; }
    public int CompletedArticlesCount { get; set; }
    public bool IsCompleted { get; set; }
}
