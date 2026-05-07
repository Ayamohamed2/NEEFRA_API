using Microsoft.AspNetCore.Mvc;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DTO;
using Restaurant.API.Controllers;

namespace NEEFRA_API.Controllers
{
    // ════════════════════════════════════════════════════════════════════
    // NearbyHotel
    // ════════════════════════════════════════════════════════════════════
    [Route("api/Museum/{museumId}/nearby-hotels")]
    [ApiController]
    public class NearbyHotelController : BaseController
    {
        private readonly INearbyHotelService _service;

        public NearbyHotelController(INearbyHotelService service) => _service = service;

        // GET: api/Museum/{museumId}/nearby-hotels
        [HttpGet]
        public async Task<IActionResult> GetAll(string museumId)
        {
            var result = await _service.GetByMuseumIdAsync(museumId);
            return HandleResult(result);
        }

        // POST: api/Museum/{museumId}/nearby-hotels
        [HttpPost]
        public async Task<IActionResult> Add(string museumId, [FromBody] CreateNearbyHotelDTO dto)
        {
            dto.MuseumId = museumId;
            var result = await _service.AddAsync(dto);
            return HandleResult(result);
        }

        // DELETE: api/Museum/{museumId}/nearby-hotels/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _service.DeleteAsync(id);
            return HandleResult(result);
        }
    }

    // ════════════════════════════════════════════════════════════════════
    // NearbyRestaurant
    // ════════════════════════════════════════════════════════════════════
    [Route("api/Museum/{museumId}/nearby-restaurants")]
    [ApiController]
    public class NearbyRestaurantController : BaseController
    {
        private readonly INearbyRestaurantService _service;

        public NearbyRestaurantController(INearbyRestaurantService service) => _service = service;

        // GET: api/Museum/{museumId}/nearby-restaurants
        [HttpGet]
        public async Task<IActionResult> GetAll(string museumId)
        {
            var result = await _service.GetByMuseumIdAsync(museumId);
            return HandleResult(result);
        }

        // POST: api/Museum/{museumId}/nearby-restaurants
        [HttpPost]
        public async Task<IActionResult> Add(string museumId, [FromBody] CreateNearbyRestaurantDTO dto)
        {
            dto.MuseumId = museumId;
            var result = await _service.AddAsync(dto);
            return HandleResult(result);
        }

        // DELETE: api/Museum/{museumId}/nearby-restaurants/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _service.DeleteAsync(id);
            return HandleResult(result);
        }
    }

    // ════════════════════════════════════════════════════════════════════
    // GiftShop
    // ════════════════════════════════════════════════════════════════════
    [Route("api/Museum/{museumId}/gift-shops")]
    [ApiController]
    public class GiftShopController : BaseController
    {
        private readonly IGiftShopService _service;

        public GiftShopController(IGiftShopService service) => _service = service;

        // GET: api/Museum/{museumId}/gift-shops
        [HttpGet]
        public async Task<IActionResult> GetAll(string museumId)
        {
            var result = await _service.GetByMuseumIdAsync(museumId);
            return HandleResult(result);
        }

        // POST: api/Museum/{museumId}/gift-shops
        [HttpPost]
        public async Task<IActionResult> Add(string museumId, [FromBody] CreateGiftShopDTO dto)
        {
            dto.MuseumId = museumId;
            var result = await _service.AddAsync(dto);
            return HandleResult(result);
        }

        // DELETE: api/Museum/{museumId}/gift-shops/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _service.DeleteAsync(id);
            return HandleResult(result);
        }
    }

    // ════════════════════════════════════════════════════════════════════
    // Cafe
    // ════════════════════════════════════════════════════════════════════
    [Route("api/Museum/{museumId}/cafes")]
    [ApiController]
    public class CafeController : BaseController
    {
        private readonly ICafeService _service;

        public CafeController(ICafeService service) => _service = service;

        // GET: api/Museum/{museumId}/cafes
        [HttpGet]
        public async Task<IActionResult> GetAll(string museumId)
        {
            var result = await _service.GetByMuseumIdAsync(museumId);
            return HandleResult(result);
        }

        // POST: api/Museum/{museumId}/cafes
        [HttpPost]
        public async Task<IActionResult> Add(string museumId, [FromBody] CreateCafeDTO dto)
        {
            dto.MuseumId = museumId;
            var result = await _service.AddAsync(dto);
            return HandleResult(result);
        }

        // DELETE: api/Museum/{museumId}/cafes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _service.DeleteAsync(id);
            return HandleResult(result);
        }
    }
}
