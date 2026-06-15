using System.Text;
using System.Text.Json;

namespace NEEFRA.Core.Services
{
    public class SpanishTtsService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://arwa-galal--seamless-m4t-english-to-spanish-fastapi-app.modal.run";

        public SpanishTtsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<byte[]> GenerateSpeechAsync(string text)
        {
            var body = new { text = text };

            var content = new StringContent(
                JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/generate", content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
