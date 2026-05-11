using Microsoft.AspNetCore.Mvc;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DTO;
using Restaurant.API.Controllers;

namespace NEEFRA_API.Controllers
{
    [Route("api/Governorate/{governorateId}/photos")]
    [ApiController]
    public class GovernoratePhotoController : BaseController
    {
        private readonly IGovernoratePhotoService _service;

        public GovernoratePhotoController(IGovernoratePhotoService service)
        {
            _service = service;
        }

        // GET: api/Governorate/{governorateId}/photos
        [HttpGet]
        public async Task<IActionResult> GetAll(string governorateId)
        {
            var result = await _service.GetByGovernorateIdAsync(governorateId, BaseUrl);
            return HandleResult(result);
        }

        // POST: api/Governorate/{governorateId}/photos
        [HttpPost]
        public async Task<IActionResult> Add(string governorateId, [FromBody] CreateGovernoratePhotoDTO dto)
        {
            dto.GovernorateId = governorateId;
            var result = await _service.AddAsync(dto);
            return HandleResult(result);
        }

        // DELETE: api/Governorate/{governorateId}/photos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _service.DeleteAsync(id);
            return HandleResult(result);
        }
    }
}
