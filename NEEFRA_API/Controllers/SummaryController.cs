using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NEEFRA.API.Helpers;
using NEEFRA.Core.DTO.Service.Summary;
using NEEFRA.Core.Interfaces.IService;
using System.Security.Claims;

namespace NEEFRA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SummaryController : ControllerBase
    {
        private readonly ISummaryService service;

        public SummaryController(ISummaryService service)
        {
            this.service = service;
        }
        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        private string BaseUrl => $"{Request.Scheme}://{Request.Host}";

        private IActionResult HandleResult<T>(SummaryDTo<T> result)
        {
            if (result.IsSuccess)
                return Ok(result);

            return result.ErrorType switch
            {
                "NotFound" => ApiResponseHelper.NotFound(result.Message),
                "Forbidden" => Forbid(result.Message!),
                "Unauthorized" => ApiResponseHelper.Unauthorized(result.Message),
                _ => ApiResponseHelper.BadRequest(result.Message)
            };
        }

        [HttpGet("Summary")]

        public async Task<IActionResult> Summary(string visitId)
        {
            var result =await service.Summary(visitId, BaseUrl);

            return HandleResult(result);
        }

        [HttpGet("SummaryForUser")]

        public async Task<IActionResult> SummaryForUser(string visitId ,string userid)
        {
            var result = await service.SummaryForUser(visitId, BaseUrl, userid);

            return HandleResult(result);
        }
    }
}
