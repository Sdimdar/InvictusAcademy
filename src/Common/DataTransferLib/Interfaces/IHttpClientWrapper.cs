using DataTransferLib.Models;

namespace DataTransferLib.Interfaces;

public interface IHttpClientWrapper
{
    HttpClient HttpClient { set; }
    public Task<DefaultResponseObject<TResponse>> GetAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri);
    public Task<DefaultResponseObject<TResponse>> GetAndReturnResponseAsync<TResponse>(string uri);
    public Task<DefaultResponseObject<TResponse>> PostAndReturnResponseAsync<TRequest, TResponse>( TRequest request, string uri);
    public Task<DefaultResponseObject<TResponse>> PostAndReturnResponseAsync<TResponse>(string uri);
    public Task<DefaultResponseObject<TResponse>> PutAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri);
    public Task<DefaultResponseObject<TResponse>> PutAndReturnResponseAsync<TResponse>(string uri);
    public Task<DefaultResponseObject<TResponse>> DeleteAndReturnResponseAsync<TRequest, TResponse>( TRequest request, string uri);
    public Task<DefaultResponseObject<TResponse>> DeleteAndReturnResponseAsync<TResponse>(string uri);
}