namespace ServicesContracts.Identity.Responses;

public class PageVm
{
    public int PageNumber { get; private set; }
    public int TotalPages { get; private set; }

    public PageVm(int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
}