using Microsoft.AspNetCore.Mvc;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DTO;
using Restaurant.API.Controllers;

namespace NEEFRA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtPieceController : BaseController
    {
        private readonly IArtPieceService _artPieceService;

        public ArtPieceController(IArtPieceService artPieceService)
        {
            _artPieceService = artPieceService;
        }

        // GET: api/ArtPiece/museum/{museumId}
        [HttpGet("museum/{museumId}")]
        public async Task<IActionResult> GetByMuseum(string museumId)
        {
            var result = await _artPieceService.GetByMuseumAsync(museumId);
            return HandleResult(result);
        }

        // POST: api/ArtPiece
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateArtPieceDTO dto)
        {
            var result = await _artPieceService.AddAsync(dto);
            return HandleResult(result);
        }
    }
}
