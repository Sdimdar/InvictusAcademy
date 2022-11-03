using AdminGateway.MVC.Services.Interfaces;
using DataTransferLib.Models;
using ExtendedHttpClient;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.Request.Requests.Commands;
using ServicesContracts.Request.Responses;

namespace AdminGateway.MVC.Services
{
    public class RequestService :IRequestService
    {
        public ExtendedHttpClient<IRequestService> ExtendedHttpClient { get; set; }
        public RequestService(ExtendedHttpClient<IRequestService> extendedHttpClient)
        {
            ExtendedHttpClient = extendedHttpClient;
        }


        public async Task<DefaultResponseObject<GetAllRequestVm>> GetAllRequestsAsync(int pageNumber, int pageSize)
        {
            return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<GetAllRequestVm>>(
                $"/Request/GetAll?pageNumber={pageNumber}&pageSize={pageSize}");
        }

        public async Task<DefaultResponseObject<string>> ChangeCalledStatusAsync(ChangeCalledStatusCommand command)
        {
            return await ExtendedHttpClient
                .PostAndReturnResponseAsync<ChangeCalledStatusCommand, DefaultResponseObject<string>>(command,
                    $"/Request/SetCalledStatus");
        }

        public async Task<DefaultResponseObject<string>> ManagerCommentAsync(ManagerCommentCommand request)
        {
            return await ExtendedHttpClient
                .PostAndReturnResponseAsync<ManagerCommentCommand, DefaultResponseObject<string>>(request,
                    "/Request/AddComment");
        }

        public async Task<DefaultResponseObject<int>> GetRequestsCountAsync()
        {
            return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<int>>("/Request/Count");
        }

    }
}