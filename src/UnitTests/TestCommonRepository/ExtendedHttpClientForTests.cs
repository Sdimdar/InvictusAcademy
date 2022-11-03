using DataTransferLib.Models;
using GlobalExceptionHandler.Exceptions;
using Newtonsoft.Json;
using System.Text;

namespace TestCommonRepository;

public class ExtendedHttpClientForTests
{
    public HttpClient HttpClient { get; set; }

    public ExtendedHttpClientForTests(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public async Task<DefaultResponseObject<TResponse>> GetAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri, CancellationToken cancellationToken = new())
    {
        var message = CreateHttpRequestMessage(HttpMethod.Get, uri, request);
        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public async Task<DefaultResponseObject<TResponse>> GetAndReturnResponseAsync<TResponse>(string uri, CancellationToken cancellationToken = new())
    {
        var message = CreateHttpRequestMessage(HttpMethod.Get, uri);
        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public async Task<DefaultResponseObject<TResponse>> PostAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri, CancellationToken cancellationToken = new())
    {
        var message = CreateHttpRequestMessage(HttpMethod.Post, uri, request);
        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public async Task<DefaultResponseObject<TResponse>> PostAndReturnResponseAsync<TResponse>(string uri, CancellationToken cancellationToken = new())
    {
        var message = CreateHttpRequestMessage(HttpMethod.Post, uri);
        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public async Task<DefaultResponseObject<TResponse>> PutAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri, CancellationToken cancellationToken = new())
    {
        var message = CreateHttpRequestMessage(HttpMethod.Put, uri, request);
        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public async Task<DefaultResponseObject<TResponse>> PutAndReturnResponseAsync<TResponse>(string uri, CancellationToken cancellationToken = new())
    {
        var message = CreateHttpRequestMessage(HttpMethod.Put, uri);
        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public async Task<DefaultResponseObject<TResponse>> DeleteAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri, CancellationToken cancellationToken = new())
    {
        var message = CreateHttpRequestMessage(HttpMethod.Delete, uri, request);
        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public async Task<DefaultResponseObject<TResponse>> DeleteAndReturnResponseAsync<TResponse>(string uri, CancellationToken cancellationToken = new())
    {
        var message = CreateHttpRequestMessage(HttpMethod.Delete, uri);
        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    private HttpRequestMessage CreateHttpRequestMessage<TRequest>(HttpMethod method, string uri, TRequest request)
    {
        return new HttpRequestMessage()
        {
            Method = method,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri),
            Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
        };
    }

    private HttpRequestMessage CreateHttpRequestMessage(HttpMethod method, string uri)
    {
        return new HttpRequestMessage()
        {
            Method = method,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri)
        };
    }

    private async Task<DefaultResponseObject<TResponse>> ExchangeAsync<TResponse>(HttpRequestMessage message,
                                                                                  CancellationToken cancellationToken)
    {
        var response = await HttpClient.SendAsync(message, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            var dataAsString = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<DefaultResponseObject<TResponse>>(dataAsString);
            if (result == null)
                throw new InvalidCastException($"Cast to {typeof(DefaultResponseObject<TResponse>)} is dropped");
            return result;
        }
        var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        throw new InternalServiceException((int)response.StatusCode, errorMessage + " Exception from: " + message.RequestUri);
    }
}