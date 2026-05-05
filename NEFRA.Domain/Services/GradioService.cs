using Microsoft.AspNetCore.Http;
using NEEFRA.Core.DTO;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Text;

namespace NEEFRA.Core.Services
{
    public class GradioService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://elneel1-elneel-grad-api.hf.space";

        public GradioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LabelData> ClassifyImageAsync(IFormFile imageFile)
        {
            // Step 1: Upload
            string filePath = await UploadImageAsync(imageFile);

            // Step 2: Queue Join
            string sessionHash = Guid.NewGuid().ToString("N")[..12];
            await QueueJoinAsync(filePath, imageFile.FileName, sessionHash);

            // Step 3: Get Result
            return await GetResultAsync(sessionHash);
        }

        private async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            using var form = new MultipartFormDataContent();
            using var stream = imageFile.OpenReadStream();
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType =
                MediaTypeHeaderValue.Parse(imageFile.ContentType);
            form.Add(fileContent, "files", imageFile.FileName);

            var response = await _httpClient.PostAsync($"{BaseUrl}/gradio_api/upload", form);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var paths = JsonSerializer.Deserialize<string[]>(json);
            return paths![0];
        }

        private async Task QueueJoinAsync(string filePath, string origName, string sessionHash)
        {
            var body = new
            {
                data = new object[]
                {
                new
                {
                    path = filePath,
                    orig_name = origName,
                    mime_type = "image/jpeg",
                    is_stream = false,
                    meta = new { _type = "gradio.FileData" }
                }
                },
                fn_index = 2,
                session_hash = sessionHash
            };

            var content = new StringContent(
                JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                $"{BaseUrl}/gradio_api/queue/join", content);
            response.EnsureSuccessStatusCode();
        }

        private async Task<LabelData> GetResultAsync(string sessionHash)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{BaseUrl}/gradio_api/queue/data?session_hash={sessionHash}");
            request.Headers.Add("Accept", "text/event-stream");

            var response = await _httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (line == null || !line.StartsWith("data: ")) continue;

                var jsonPart = line["data: ".Length..];
                using var doc = JsonDocument.Parse(jsonPart);
                var msg = doc.RootElement.GetProperty("msg").GetString();

                if (msg == "process_completed")
                {
                    var dataArr = doc.RootElement
                        .GetProperty("output")
                        .GetProperty("data")[0];

                    return JsonSerializer.Deserialize<LabelData>(
                        dataArr.GetRawText(),
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        })!;
                }
            }

            throw new Exception("No result received from model");
        }
    }
    }