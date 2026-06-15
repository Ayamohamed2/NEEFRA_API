using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NEEFRA.API.Helpers;
using NEEFRA.Core.DTO.AIDescription;
using NEEFRA.Core.Interfaces.IService;
using Restaurant.API.Controllers;

namespace NEEFRA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIDescriptionController : BaseController
    {
        private readonly IAIService service;
        private readonly IWebHostEnvironment env;

        public AIDescriptionController(IAIService Service, IWebHostEnvironment env)
        {
            service = Service;
            this.env = env;
        }
  
        [HttpPost("AIDescription")]
        public async Task<IActionResult> AIDescription(AIDescriptionDTO dto)
        {
            var result = await service.AIDescription(dto, UserId,env,BaseUrl);

            return HandleResult(result);
        }
    

    }
}
