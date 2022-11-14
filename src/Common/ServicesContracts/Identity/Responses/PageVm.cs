namespace ServicesContracts.Identity.Responses;

public class PageVm
{
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }

    public PageVm()
    {

    }

    public PageVm(int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
}