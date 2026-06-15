using Microsoft.AspNetCore.Mvc;
using YourApp.Services;
using System.Threading.Tasks;

namespace YourApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SummarizeController : ControllerBase
    {
        private readonly ISummarizeService _summarizeService;

        public SummarizeController(ISummarizeService summarizeService)
        {
            _summarizeService = summarizeService;
        }

        /// <summary>
        /// يستقبل نص وبيبعته لـ HuggingFace API ويرجع التلخيص.
        /// اللغات المدعومة: "Arabic" أو "English"
        /// POST api/summarize
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Summarize([FromBody] SummarizeRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Paragraph))
                return BadRequest(new
                {
                    message = "paragraph is required.",
                    supportedLanguages = new[] { "Arabic", "English" }
                });

            if (!SupportedLanguages.IsValid(request.Language))
                return BadRequest(new
                {
                    message = $"Language '{request.Language}' is not supported.",
                    supportedLanguages = new[] { "Arabic", "English" }
                });

            var result = await _summarizeService.SummarizeAsync(request);

            if (result == null)
                return StatusCode(502, new { message = "No response from the Summarize API." });

            return Ok(result);
        }
    }
}
