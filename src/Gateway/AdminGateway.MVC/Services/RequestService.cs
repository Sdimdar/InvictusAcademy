using AdminGateway.MVC.Services.Interfaces;
using DataTransferLib.Interfaces;
using DataTransferLib.Models;
using ServicesContracts.Request.Requests.Commands;
using ServicesContracts.Request.Responses;

namespace AdminGateway.MVC.Services
{
    public class RequestService : IRequestService
    {
        private readonly IHttpClientWrapper _httpClient;

        public RequestService(HttpClient httpClient, IHttpClientWrapper httpClientWrapper)
        {
            _httpClient = httpClientWrapper ?? throw new ArgumentNullException(nameof(httpClientWrapper));
            _httpClient.HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<DefaultResponseObject<GetAllRequestVm>> GetAllRequestsAsync(int pageNumber, int pageSize)
        {
            return await _httpClient.GetAndReturnResponseAsync<GetAllRequestVm>($"/Request/GetAll?pageNumber={pageNumber}&pageSize={pageSize}", new CancellationToken());
        }

        public async Task<DefaultResponseObject<string>> ChangeCalledStatusAsync(ChangeCalledStatusCommand command)
        {
            return await _httpClient.PostAndReturnResponseAsync<ChangeCalledStatusCommand, string>(command, $"/Request/SetCalledStatus", new CancellationToken());
        }

        public async Task<DefaultResponseObject<string>> ManagerCommentAsync(ManagerCommentCommand request)
        {
            return await _httpClient.PostAndReturnResponseAsync<ManagerCommentCommand, string>(request, "/Request/AddComment", new CancellationToken());
        }

        public async Task<DefaultResponseObject<int>> GetRequestsCountAsync()
        {
            return await _httpClient.GetAndReturnResponseAsync<int>("/Request/Count", new CancellationToken());
        }
    }
}