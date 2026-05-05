using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA.Core.Services;
using Restaurant.API.Controllers;

namespace NEEFRA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartRouteController : BaseController
    {
        private readonly ISmartRouteService service;

        public SmartRouteController(ISmartRouteService service)
        {
            this.service = service;
        }

        [HttpGet("Route")]
        public async Task<IActionResult> GetRoute([FromQuery] string? groupId = null)
        {
            var result = await service.GetRouteAsync(UserId, groupId);
            return HandleResult(result);
        }
    }
}
