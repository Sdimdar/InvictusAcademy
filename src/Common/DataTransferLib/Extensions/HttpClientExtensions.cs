using DataTransferLib.Models;
using Newtonsoft.Json;
using System.Text;

namespace DataTransferLib.Extensions;

public static class HttpClientExtensions
{
    public async static Task<DefaultResponseObject<TResponse>> GetAndReturnResponseAsync<TRequest, TResponse>(this HttpClient client, TRequest request, string uri)
    {
        var message = new HttpRequestMessage()
        { 
            Method = HttpMethod.Get,
            RequestUri = new Uri(client.BaseAddress!, uri),
            Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
        };

        return await client.ExchangeAsync<TResponse>(message);
    }

    public async static Task<DefaultResponseObject<TResponse>> GetAndReturnResponseAsync<TResponse>(this HttpClient client, string uri)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(client.BaseAddress!, uri)
        };

        return await client.ExchangeAsync<TResponse>(message);
    }

    public async static Task<DefaultResponseObject<TResponse>> PostAndReturnResponseAsync<TRequest, TResponse>(this HttpClient client, TRequest request, string uri)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(client.BaseAddress!, uri),
            Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
        };

        return await client.ExchangeAsync<TResponse>(message);
    }

    public async static Task<DefaultResponseObject<TResponse>> PostAndReturnResponseAsync<TResponse>(this HttpClient client, string uri)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(client.BaseAddress!, uri)
        };

        return await client.ExchangeAsync<TResponse>(message);
    }

    public async static Task<DefaultResponseObject<TResponse>> PutAndReturnResponseAsync<TRequest, TResponse>(this HttpClient client, TRequest request, string uri)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(client.BaseAddress!, uri),
            Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
        };

        return await client.ExchangeAsync<TResponse>(message);
    }

    public async static Task<DefaultResponseObject<TResponse>> PutAndReturnResponseAsync<TResponse>(this HttpClient client, string uri)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(client.BaseAddress!, uri)
        };

        return await client.ExchangeAsync<TResponse>(message);
    }

    public async static Task<DefaultResponseObject<TResponse>> DeleteAndReturnResponseAsync<TRequest, TResponse>(this HttpClient client, TRequest request, string uri)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(client.BaseAddress!, uri),
            Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
        };

        return await client.ExchangeAsync<TResponse>(message);
    }

    public async static Task<DefaultResponseObject<TResponse>> DeleteAndReturnResponseAsync<TResponse>(this HttpClient client, string uri)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(client.BaseAddress!, uri)
        };

        return await client.ExchangeAsync<TResponse>(message);
    }

    private async static Task<DefaultResponseObject<TResponse>> ExchangeAsync<TResponse>(this HttpClient client, HttpRequestMessage message)
    {
        try
        {
            var response = await client.SendAsync(message);
            response.EnsureSuccessStatusCode();
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<DefaultResponseObject<TResponse>>(dataAsString);
            if (result == null) throw new InvalidCastException($"Cast to {typeof(DefaultResponseObject<TResponse>)} is dropped");
            return result;
        }
        catch (Exception ex)
        {
            return new DefaultResponseObject<TResponse>() { Errors = new string[] { "Internal server request error \n" + ex.Message } };
        }
    }
}
