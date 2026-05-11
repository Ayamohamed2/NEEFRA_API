using IdentityModel;
using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Mvc;
using NEEFRA.Core.Services;
using NEEFRA.Domain.IReposatory;

namespace NEEFRA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassifyController : ControllerBase
    {
        private readonly GradioService _gradioService;
        private readonly IUnitOfWork unit;

        public ClassifyController(GradioService gradioService, IUnitOfWork unit)
        {
            _gradioService = gradioService;
            this.unit = unit;
        }
        protected string BaseUrl => $"{Request.Scheme}://{Request.Host}"+"/";

        [HttpPost]
        [Consumes("multipart/form-data")]  // ← السطر ده بس اللي محتاج تضيفيه
        public async Task<IActionResult> Predict(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest("No image provided");

            try
            {
                var result = await _gradioService.ClassifyImageAsync(image);
                var Labelimg = (await unit.ArtPiece.GetByFilterAsync(p => p.Name == result.Label)).ImageUrl;
                result.ImageUrl = string.IsNullOrEmpty(Labelimg)
                    ? null
                : BaseUrl + Labelimg;

                for (int i = 0; i < result.Confidences.Count(); i++)
                {
                    var img = (await unit.ArtPiece.GetByFilterAsync(p => p.Name == result.Confidences[i].Label)).ImageUrl;
                    result.Confidences[i].ImageUrl = string.IsNullOrEmpty(img)
                    ? null
                : BaseUrl + img;
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}