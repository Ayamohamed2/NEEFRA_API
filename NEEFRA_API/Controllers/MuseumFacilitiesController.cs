using Microsoft.AspNetCore.Mvc;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DTO;
using Restaurant.API.Controllers;

namespace NEEFRA_API.Controllers
{
    [Route("api/Museum/{museumId}/facilities")]
    [ApiController]
    public class MuseumFacilitiesController : BaseController
    {
        private readonly IMuseumFacilitiesService _service;

        public MuseumFacilitiesController(IMuseumFacilitiesService service)
        {
            _service = service;
        }

        // GET: api/Museum/{museumId}/facilities
        [HttpGet]
        public async Task<IActionResult> Get(string museumId)
        {
            var result = await _service.GetByMuseumIdAsync(museumId,BaseUrl);
            return HandleResult(result);
        }

        // POST: api/Museum/{museumId}/facilities
        [HttpPost]
        public async Task<IActionResult> Add(string museumId, [FromBody] CreateMuseumFacilitiesDTO dto)
        {
            dto.MuseumId = museumId;
            var result = await _service.AddAsync(dto);
            return HandleResult(result);
        }

        // PUT: api/Museum/{museumId}/facilities
        [HttpPut]
        public async Task<IActionResult> Update(string museumId, [FromBody] UpdateMuseumFacilitiesDTO dto)
        {
            var result = await _service.UpdateAsync(museumId, dto);
            return HandleResult(result);
        }
    }
}
