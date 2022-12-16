using ServicesContracts.Payments.Models;

namespace ServicesContracts.Payments.Response;

public class PaymentsPaginationVm
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public List<PaymentsVm> Payments { get; set; }
    public int PaymentsCount { get; set; }
    public bool HasPreviousPage
    {
        get
        {
            return (PageNumber > 1);
        }
    }
 
    public bool HasNextPage
    {
        get
        {
            return (PageNumber < TotalPages);
        }
    }
}