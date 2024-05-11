namespace PokemonAPI.IntegrationTests;

internal static class ApiMessageSender
{
    private static readonly HttpClient HttpClient = new();

    internal static async Task<HttpResponseMessage> SendRequest(Uri requestUri)
    {
        var message = new HttpRequestMessage();
        message.Method = HttpMethod.Get;
        message.RequestUri = requestUri;

        return await HttpClient.SendAsync(message);
    } 
}