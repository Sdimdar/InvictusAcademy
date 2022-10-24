using Newtonsoft.Json;

namespace AdminGateway.MVC.HttpClientExtensions;

public static class HttpClientExtensions
{
    public static async Task<T?> ReadContentAs<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Something was wrong calling the API : {response.ReasonPhrase}");

        string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var data = JsonConvert.DeserializeObject<T>(dataAsString, new JsonSerializerSettings() { });
        return data;
    }
}