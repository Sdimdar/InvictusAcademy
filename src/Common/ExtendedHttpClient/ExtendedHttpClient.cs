﻿using ExtendedHttpClient.Exceptions;
using Newtonsoft.Json;
using System.Text;

namespace ExtendedHttpClient;

public class ExtendedHttpClient<T>
{
    public HttpClient HttpClient { get; private set; }

    public ExtendedHttpClient(IHttpClientFactory httpClientFactory, HttpClientOptions<T> options)
    {
        HttpClient = httpClientFactory.CreateClient();
        options.Configure(HttpClient);
    }

    public virtual async Task<TResponse> GetAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri, CancellationToken cancellationToken = new())
    {
        var message = CreateHttpRequestMessage(HttpMethod.Get, uri, request);
        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public virtual async Task<TResponse> GetAndReturnResponseAsync<TResponse>(string uri, CancellationToken cancellationToken = new())
    {
        var message = CreateHttpRequestMessage(HttpMethod.Get, uri);
        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public virtual async Task<TResponse> PostAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri, CancellationToken cancellationToken = new())
    {
        var message = CreateHttpRequestMessage(HttpMethod.Post, uri, request);
        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public virtual async Task<TResponse> PostAndReturnResponseAsync<TResponse>(string uri, CancellationToken cancellationToken = new())
    {
        var message = CreateHttpRequestMessage(HttpMethod.Post, uri);
        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public virtual async Task<TResponse> PutAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri, CancellationToken cancellationToken = new())
    {
        var message = CreateHttpRequestMessage(HttpMethod.Put, uri, request);
        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public virtual async Task<TResponse> PutAndReturnResponseAsync<TResponse>(string uri, CancellationToken cancellationToken = new())
    {
        var message = CreateHttpRequestMessage(HttpMethod.Put, uri);
        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public virtual async Task<TResponse> DeleteAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri, CancellationToken cancellationToken = new())
    {
        var message = CreateHttpRequestMessage(HttpMethod.Delete, uri, request);
        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public virtual async Task<TResponse> DeleteAndReturnResponseAsync<TResponse>(string uri, CancellationToken cancellationToken = new())
    {
        var message = CreateHttpRequestMessage(HttpMethod.Delete, uri);
        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    protected virtual HttpRequestMessage CreateHttpRequestMessage<TRequest>(HttpMethod method, string uri, TRequest request)
    {
        return new HttpRequestMessage()
        {
            Method = method,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri),
            Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
        };
    }

    protected virtual HttpRequestMessage CreateHttpRequestMessage(HttpMethod method, string uri)
    {
        return new HttpRequestMessage()
        {
            Method = method,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri)
        };
    }

    protected virtual async Task<TResponse> ExchangeAsync<TResponse>(HttpRequestMessage message,
                                                                                  CancellationToken cancellationToken)
    {
        var response = await HttpClient.SendAsync(message, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            var dataAsString = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<TResponse>(dataAsString);
            if (result == null)
                throw new InvalidCastException($"Cast to {typeof(TResponse)} is dropped");
            return result;
        }
        var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        throw new InternalServiceException((int)response.StatusCode, errorMessage + " Exception from: " + message.RequestUri);
    }
}
