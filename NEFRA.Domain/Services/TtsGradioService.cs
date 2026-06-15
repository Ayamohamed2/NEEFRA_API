using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace NEEFRA.Core.Services
{
    public class TtsService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://elneel1-elneel-eng-model2.hf.space";

        public TtsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<byte[]> GenerateSpeechAsync(string text, string voice = "af_sarah", double speed = 1.0)
        {
            var body = new
            {
                text = text,
                voice = voice,
                speed = speed
            };

            var content = new StringContent(
                JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/tts", content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<List<string>> GetVoicesAsync()
        {
            var response = await _httpClient.GetStringAsync($"{BaseUrl}/voices");
            using var doc = JsonDocument.Parse(response);
            return doc.RootElement
                .GetProperty("voices")
                .EnumerateArray()
                .Select(v => v.GetString()!)
                .ToList();
        }
    }
}