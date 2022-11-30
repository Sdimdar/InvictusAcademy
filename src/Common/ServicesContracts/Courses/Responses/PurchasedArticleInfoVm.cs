namespace ServicesContracts.Courses.Responses;

public class PurchasedArticleInfoVm
{
    public int Order { get; set; }
    public string Title { get; set; }
    public string VideoLink { get; set; }
    public string Text { get; set; }
    public bool IsCompleted { get; set; }
    public ShortModuleInfoVm ModuleInfo { get; set; }
    public List<ShortArticleInfoVm> Articles { get; set; }
}
