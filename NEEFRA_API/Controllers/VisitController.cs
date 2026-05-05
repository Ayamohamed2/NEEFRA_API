using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DTO;
using Restaurant.API.Controllers;

namespace NEEFRA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VisitController : BaseController
    {
        private readonly IVisitService visitService;

        public VisitController(IVisitService visitService)
        {
            this.visitService = visitService;
        }

        // ════════════════════════════════════════════════════════════════════
        // Visit Operations
        // ════════════════════════════════════════════════════════════════════

        [HttpPost("CheckAndStart")]
        public async Task<IActionResult> CheckAndStart([FromBody] CheckLocationDTO dto)
        {
            var result = await visitService.CheckAndStartAsync(UserId, dto);
            return HandleResult(result);
        }

        [HttpPost("End/{visitId}")]
        public async Task<IActionResult> EndVisit(string visitId)
        {
            var result = await visitService.EndVisitAsync(UserId, visitId);
            return HandleResult(result);
        }

        [HttpGet("MyVisits")]
        public async Task<IActionResult> GetMyVisits()
        {
            var result = await visitService.GetMyVisitsAsync(UserId);
            return HandleResult(result);
        }

        [HttpGet("MySoloVisits")]
        public async Task<IActionResult> GetMySoloVisits()
        {
            var result = await visitService.GetMySoloVisitsAsync(UserId);
            return HandleResult(result);
        }

        [HttpGet("MyGroupVisits")]
        public async Task<IActionResult> GetMyGroupVisits()
        {
            var result = await visitService.GetMyGroupVisitsAsync(UserId);
            return HandleResult(result);
        }
    }
}
