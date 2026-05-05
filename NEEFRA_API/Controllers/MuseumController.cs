using Microsoft.AspNetCore.Mvc;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DTO;
using Restaurant.API.Controllers;

namespace NEEFRA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MuseumController : BaseController
    {
        private readonly IMuseumService _museumService;

        public MuseumController(IMuseumService museumService)
        {
            _museumService = museumService;
        }

        // GET: api/Museum
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _museumService.GetAllAsync();
            return HandleResult(result);
        }

        // GET: api/Museum/governorate/{governorateId}
        [HttpGet("governorate/{governorateId}")]
        public async Task<IActionResult> GetByGovernorate(string governorateId)
        {
            var result = await _museumService.GetByGovernorateAsync(governorateId);
            return HandleResult(result);
        }

        // POST: api/Museum
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateMuseumDTO dto)
        {
            var result = await _museumService.AddAsync(dto);
            return HandleResult(result);
        }
    }
}
