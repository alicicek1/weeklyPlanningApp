using System.Xml.Serialization;

namespace toDoApp.Command.Client;

public class CustomHttpClient : IHttpClient
{
    private readonly HttpClient _httpClient = new();

    public async Task<T?> GetAsync<T>(string url)
    {
        using var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        return HandleResponse<T>(content, response.Content.Headers.ContentType?.MediaType!);
    }

    private static T? HandleResponse<T>(string content, string contentType)
    {
        if (string.IsNullOrEmpty(contentType))
        {
            throw new InvalidOperationException("Content type is missing in the HTTP response.");
        }

        if (contentType.Contains("json", StringComparison.OrdinalIgnoreCase))
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
        }
        else if (contentType.Contains("xml", StringComparison.OrdinalIgnoreCase))
        {
            var serializer = new XmlSerializer(typeof(T));
            using var reader = new StringReader(content);
            return (T) serializer.Deserialize(reader)!;
        }
        else
        {
            throw new NotSupportedException($"Content type '{contentType}' is not supported.");
        }
    }
}