using System.Text;
using DataTransferLib.Interfaces;
using DataTransferLib.Models;
using Newtonsoft.Json;

namespace DataTransferLib;

public class HttpClientWrapper : IHttpClientWrapper
{
    public HttpClient HttpClient { private get; set; }

    public HttpClientWrapper()
    {
        HttpClient = new HttpClient();
    }
    
    public HttpClientWrapper(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public async Task<DefaultResponseObject<TResponse>> GetAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri)
    {
        var message = new HttpRequestMessage()
        { 
            Method = HttpMethod.Get,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri),
            Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
        };

        return await ExchangeAsync<TResponse>(message, new CancellationToken());
    }

    public async Task<DefaultResponseObject<TResponse>> GetAndReturnResponseAsync<TResponse>(string uri)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri)
        };

        return await ExchangeAsync<TResponse>(message, new CancellationToken());
    }

    public async Task<DefaultResponseObject<TResponse>> PostAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri),
            Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
        };

        return await ExchangeAsync<TResponse>(message, new CancellationToken());
    }

    public async Task<DefaultResponseObject<TResponse>> PostAndReturnResponseAsync<TResponse>(string uri)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri)
        };

        return await ExchangeAsync<TResponse>(message, new CancellationToken());
    }

    public async Task<DefaultResponseObject<TResponse>> PutAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri),
            Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
        };

        return await ExchangeAsync<TResponse>(message, new CancellationToken());
    }

    public async Task<DefaultResponseObject<TResponse>> PutAndReturnResponseAsync<TResponse>(string uri)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri)
        };

        return await ExchangeAsync<TResponse>(message, new CancellationToken());
    }

    public async Task<DefaultResponseObject<TResponse>> DeleteAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri),
            Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
        };

        return await ExchangeAsync<TResponse>(message, new CancellationToken());
    }

    public async Task<DefaultResponseObject<TResponse>> DeleteAndReturnResponseAsync<TResponse>(string uri)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri)
        };

        return await ExchangeAsync<TResponse>(message, new CancellationToken());
    }
    
    public async Task<DefaultResponseObject<TResponse>> GetAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri, CancellationToken cancellationToken)
    {
        var message = new HttpRequestMessage()
        { 
            Method = HttpMethod.Get,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri),
            Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
        };

        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public async Task<DefaultResponseObject<TResponse>> GetAndReturnResponseAsync<TResponse>(string uri, CancellationToken cancellationToken)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri)
        };

        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public async Task<DefaultResponseObject<TResponse>> PostAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri, CancellationToken cancellationToken)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri),
            Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
        };

        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public async Task<DefaultResponseObject<TResponse>> PostAndReturnResponseAsync<TResponse>(string uri, CancellationToken cancellationToken)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri)
        };

        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public async Task<DefaultResponseObject<TResponse>> PutAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri, CancellationToken cancellationToken)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri),
            Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
        };

        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public async Task<DefaultResponseObject<TResponse>> PutAndReturnResponseAsync<TResponse>(string uri, CancellationToken cancellationToken)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri)
        };

        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public async Task<DefaultResponseObject<TResponse>> DeleteAndReturnResponseAsync<TRequest, TResponse>(TRequest request, string uri, CancellationToken cancellationToken)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri),
            Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
        };

        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    public async Task<DefaultResponseObject<TResponse>> DeleteAndReturnResponseAsync<TResponse>(string uri, CancellationToken cancellationToken)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(HttpClient.BaseAddress!, uri)
        };

        return await ExchangeAsync<TResponse>(message, cancellationToken);
    }

    private async Task<DefaultResponseObject<TResponse>> ExchangeAsync<TResponse>(HttpRequestMessage message, CancellationToken cancellationToken)
    {
        try
        {
            var response = await HttpClient.SendAsync(message, cancellationToken);
            response.EnsureSuccessStatusCode();
            var dataAsString = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<DefaultResponseObject<TResponse>>(dataAsString);
            if (result == null) throw new InvalidCastException($"Cast to {typeof(DefaultResponseObject<TResponse>)} is dropped");
            return result;
        }
        catch (Exception ex)
        {
            return new DefaultResponseObject<TResponse>() { Errors = new[] { "Internal server request error \n" + ex.Message } };
        }
    }
}
