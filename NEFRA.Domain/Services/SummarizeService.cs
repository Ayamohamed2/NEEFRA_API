using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace YourApp.Services
{
    // اللغات المدعومة من الـ API
    public static class SupportedLanguages
    {
        public static readonly HashSet<string> All = new(System.StringComparer.OrdinalIgnoreCase)
        {
            "Arabic",
            "English"
        };

        public static bool IsValid(string language) => All.Contains(language);
    }

    // DTO للـ Request اللي هنبعته للـ HuggingFace API
    public class SummarizeRequest
    {
        [JsonPropertyName("paragraph")]
        public string Paragraph { get; set; } = string.Empty;

        /// <summary>
        /// اللغة المدعومة: "Arabic" أو "English"
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; } = "Arabic";
    }

    // DTO للـ Response اللي هنستقبله من الـ API
    public class SummarizeResponse
    {
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("language")]
        public string Language { get; set; } = string.Empty;
    }

    public interface ISummarizeService
    {
        Task<SummarizeResponse?> SummarizeAsync(SummarizeRequest request);
    }

    public class SummarizeService : ISummarizeService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://ArwaGalal-genqwen.hf.space/summarize";

        public SummarizeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SummarizeResponse?> SummarizeAsync(SummarizeRequest request)
        {
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(ApiUrl, jsonContent);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<SummarizeResponse>();
            return result;
        }
    }
}
