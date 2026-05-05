using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DTO;
using Restaurant.API.Controllers;

namespace NEEFRA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavouriteController : BaseController
    {
        private readonly IFavouriteService _favouriteService;

        public FavouriteController(IFavouriteService favouriteService)
        {
            _favouriteService = favouriteService;
        }

        // POST: api/Favourite
        [HttpPost]
        public async Task<IActionResult> AddFavourite([FromBody] AddFavouriteDTO dto)
        {
            var result = await _favouriteService.AddFavouriteAsync(UserId, dto);
            return HandleResult(result);
        }

        // GET: api/Favourite
        [HttpGet]
        public async Task<IActionResult> GetMyFavourites()
        {
            var result = await _favouriteService.GetMyFavouritesAsync(UserId);
            return HandleResult(result);
        }
    }
}
