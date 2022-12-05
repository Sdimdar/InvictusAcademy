using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.Request.Requests.Commands;
using ServicesContracts.Request.Responses;

namespace AdminGateway.MVC.Services.Interfaces;

public interface IRequestService : IUseExtendedHttpClient<IRequestService>
{
    Task<DefaultResponseObject<GetAllRequestVm>> GetAllRequestsAsync(int pageNumber, int pageSize);

    Task<DefaultResponseObject<string>> ChangeCalledStatusAsync(ChangeCalledStatusCommand command);

    Task<DefaultResponseObject<string>> ManagerCommentAsync(ManagerCommentCommand request);

    Task<DefaultResponseObject<int>> GetRequestsCountAsync();
}