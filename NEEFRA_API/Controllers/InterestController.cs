using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NEEFRA.API.Helpers;
using NEEFRA.Core.DTO.Interests;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Interfaces.IService;
using Restaurant.API.Controllers;
using System.Security.Claims;

namespace NEEFRA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InterestController : BaseController
    {
        private readonly IInterestService InterestService;

        public InterestController(IInterestService InterestService)
        {
            this.InterestService = InterestService;
        }
       
        [HttpGet("GetInterests")]
        public async Task<IActionResult> GetInterests()
        {
            var result = await InterestService.GetIntersets();

            return HandleResult(result);
        }

        [HttpGet("Get_U_G_Interests")]
        public async Task<IActionResult> Get_U_G_Interests(string? GroupId)
        {
            var result = await InterestService.Get_U_G_Interests(GroupId, UserId);

            return HandleResult(result);
        }


        [HttpPost("AddInterest")]
        public async Task<IActionResult> AddInterest(U_G_InterestDTO dto)
        {
            var result = await InterestService.AddInterst(dto, UserId);

            return HandleResult(result);
        }

        [HttpDelete("RemoveInterest")]
        public async Task<IActionResult> RemoveInterest(U_G_InterestDTO dto)
        {
            var result = await InterestService.DeleteInterst(dto, UserId);
            return HandleResult(result);
        }

    }
}
