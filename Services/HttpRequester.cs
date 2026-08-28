using DBStations.Configuration;
using Microsoft.Extensions.Options;

namespace DBStations.Services
{
    public class HttpRequester(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> options)
    {
        private readonly ApiSettings _options = options.Value;

        // This needs to inside a try catch block to handle exceptions
        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            HttpClient client = httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Add("DB-Api-Key", _options.ApiKey);
            client.DefaultRequestHeaders.Add("DB-Client-Id", _options.ClientId);

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return response;
        }
    }
}
