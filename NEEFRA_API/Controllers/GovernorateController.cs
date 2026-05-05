using Microsoft.AspNetCore.Mvc;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DTO;
using Restaurant.API.Controllers;

namespace NEEFRA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernorateController : BaseController
    {
        private readonly IGovernorateService _governorateService;

        public GovernorateController(IGovernorateService governorateService)
        {
            _governorateService = governorateService;
        }

        // GET: api/Governorate
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _governorateService.GetAllAsync();
            return HandleResult(result);
        }

        // POST: api/Governorate
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGovernorateDTO dto)
        {
            var result = await _governorateService.AddAsync(dto);
            return HandleResult(result);
        }
    }
}
