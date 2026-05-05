using Microsoft.AspNetCore.Mvc;
using NEEFRA.Core.Services;

namespace NEEFRA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassifyController : ControllerBase
    {
        private readonly GradioService _gradioService;

        public ClassifyController(GradioService gradioService)
        {
            _gradioService = gradioService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]  // ← السطر ده بس اللي محتاج تضيفيه
        public async Task<IActionResult> Predict(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest("No image provided");

            try
            {
                var result = await _gradioService.ClassifyImageAsync(image);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}