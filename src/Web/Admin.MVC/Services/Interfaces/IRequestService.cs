using Admin.MVC.ViewModels;
using DataTransferLib.Models;
using ServicesContracts.Request.Requests.Commands;
using ServicesContracts.Request.Responses;

namespace Admin.MVC.Services.Interfaces;

public interface IRequestService
{ 
    public Task<DefaultResponseObject<GetAllRequestVm>> GetAllRequestsAsync(int pageNumber, int pageSize);
    public Task<DefaultResponseObject<string>> ChangeCalledStatusAsync(ChangeCalledStatusCommand command);
    public Task<DefaultResponseObject<string>> ManagerCommentAsync(ManagerCommentCommand request);
    public Task<DefaultResponseObject<int>> GetRequestsCountAsync();
}