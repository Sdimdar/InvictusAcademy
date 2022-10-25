using AdminGateway.MVC.Services.Interfaces;
using DataTransferLib.Interfaces;
using DataTransferLib.Models;
using ServicesContracts.Identity.Responses;

namespace AdminGateway.MVC.Services
{
    public class GetUsers : IGetUsers
    {
        private readonly IHttpClientWrapper _httpClient;

        public GetUsers(HttpClient httpClient, IHttpClientWrapper httpClientWrapper)
        {
            _httpClient = httpClientWrapper ?? throw new ArgumentNullException(nameof(httpClientWrapper)); 
            _httpClient.HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<DefaultResponseObject<UsersVm>> GetUsersAsync()
        {
            return await _httpClient.GetAndReturnResponseAsync<UsersVm>("/User/GetUsersData", new CancellationToken());
        }

    }
}