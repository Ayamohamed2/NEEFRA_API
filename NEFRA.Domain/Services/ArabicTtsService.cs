using System.Text;
using System.Text.Json;

namespace NEEFRA.Core.Services
{
    public class ArabicTtsService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://elneel1-arabic-grad-model.hf.space";

        public ArabicTtsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<byte[]> TranslateAndSpeakAsync(string englishText)
        {
            // Step 1: Call /gradio_api/call/v2/translate_and_speak → get event_id
            string eventId = await CallApiAsync(englishText);

            // Step 2: Poll result using event_id
            string audioPath = await GetResultAsync(eventId);

            // Step 3: Download the audio
            return await DownloadAudioAsync(audioPath);
        }

        private async Task<string> CallApiAsync(string text)
        {
            var body = new { text = text };

            var content = new StringContent(
                JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                $"{BaseUrl}/gradio_api/call/v2/translate_and_speak", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            return doc.RootElement.GetProperty("event_id").GetString()!;
        }

        private async Task<string> GetResultAsync(string eventId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{BaseUrl}/gradio_api/call/translate_and_speak/{eventId}");
            request.Headers.Add("Accept", "text/event-stream");

            var response = await _httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);

            string? eventType = null;

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (line == null) continue;

                if (line.StartsWith("event: "))
                {
                    eventType = line["event: ".Length..].Trim();
                    continue;
                }

                if (line.StartsWith("data: ") && eventType == "complete")
                {
                    var jsonPart = line["data: ".Length..];
                    using var doc = JsonDocument.Parse(jsonPart);

                    // الـ response بيرجع array — العنصر الأول هو الـ audio object
                    var audioObj = doc.RootElement[0];

                    if (audioObj.TryGetProperty("url", out var urlProp) &&
                        urlProp.ValueKind != JsonValueKind.Null)
                        return urlProp.GetString()!;

                    if (audioObj.TryGetProperty("path", out var pathProp))
                        return pathProp.GetString()!;
                }
            }

            throw new Exception("No audio result received from Arabic TTS model");
        }

        private async Task<byte[]> DownloadAudioAsync(string filePath)
        {
            if (filePath.StartsWith("http"))
                return await _httpClient.GetByteArrayAsync(filePath);

            return await _httpClient.GetByteArrayAsync(
                $"{BaseUrl}/gradio_api/file={filePath}");
        }
    }
}