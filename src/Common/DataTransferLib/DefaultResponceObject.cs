namespace DataTransferLib;

public class DefaultResponceObject<T> where T : class
{
    public T? Data { get; set; }
    public string? SuccessMessage { get; set; }
    public string[]? Errors { get; set; }
    public ValidationError[]? ValidationErrors { get; set; }
}
