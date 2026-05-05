using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NEEFRA.API.Helpers;
using NEEFRA.Core.DTO.AIDescription;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Interfaces.IService;
using Restaurant.API.Controllers;
using System.Security.Claims;

namespace NEEFRA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AIDescriptionController : BaseController
    {
        private readonly IAIService service;

        public AIDescriptionController(IAIService Service)
        {
            service = Service;
        }
  
        [HttpPost("AIDescription")]
        public async Task<IActionResult> AIDescription(AIDescriptionDTO dto)
        {
            var result = await service.AIDescription(dto, UserId);

            return HandleResult(result);
        }

    }
}
