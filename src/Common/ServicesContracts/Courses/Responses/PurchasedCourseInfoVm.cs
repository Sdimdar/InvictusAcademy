namespace ServicesContracts.Courses.Responses;

public class PurchasedCourseInfoVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CompletedModulesCount { get; set; }
    public ShortModuleInfoVm? NextLearingModule { get; set; }
    public ShortArticleInfoVm? NextLearningArticle { get; set; }
    public List<PurchasedModuleInfoVm> Modules { get; set; }
}