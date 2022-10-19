using Admin.MVC.ViewModels;
using DataTransferLib.Models;
using ServicesContracts.Request.Requests.Commands;
using ServicesContracts.Request.Responses;

namespace Admin.MVC.Services.Interfaces;

public interface IRequestService
{ 
    public Task<DefaultResponceObject<GetAllRequestVm>> GetAllRequestsAsync(int pageNumber, int pageSize);
    public Task<DefaultResponceObject<string>> ChangeCalledStatusAsync(ChangeCalledStatusCommand command);
    public Task<DefaultResponceObject<string>> ManagerCommentAsync(ManagerCommentCommand request);
    public Task<DefaultResponceObject<int>> GetRequestsCountAsync();
}