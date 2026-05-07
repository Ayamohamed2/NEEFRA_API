using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NEEFRA.Core.Interfaces.IService;
using System.Security.Claims;

namespace NEEFRA_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SmartRouteController : ControllerBase
    {
        private readonly ISmartRouteService _routeService;

        public SmartRouteController(ISmartRouteService routeService)
        {
            _routeService = routeService;
        }

        // GET api/SmartRoute?groupId=xxx&lat=30.0444&lng=31.2357
        // groupId → optional, pass for group visit
        // lat/lng → visitor's current GPS position (required for shortest path)
        [HttpGet]
        public async Task<IActionResult> GetRoute(
            [FromQuery] string? groupId,
            [FromQuery] double lat,
            [FromQuery] double lng)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            if (lat == 0 || lng == 0)
                return BadRequest("lat and lng are required");

            var result = await _routeService.GetRouteAsync(userId, groupId, lat, lng);

            if (!result.IsSuccess)
                return result.ErrorType == "NotFound"
                    ? NotFound(result.Message)
                    : BadRequest(result.Message);

            return Ok(result.Data);
        }
    }
}
