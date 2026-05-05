using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NEEFRA.Core.DTO.Profile;

using NEEFRA.Core.Interfaces.IService;
using Restaurant.API.Controllers;
using System.Security.Claims;

namespace Villa_API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfileController : BaseController
    {
        private readonly IUserProfileService userProfileService;
        private readonly IWebHostEnvironment env;

        public UserProfileController(IUserProfileService userProfileService, IWebHostEnvironment env)
        {
            this.userProfileService = userProfileService;
            this.env = env;
        }

 

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var result = await userProfileService.GetProfileAsync(userId, baseUrl);
            return HandleResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(UserProfileDTO profileDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var result = await userProfileService.UpdateProfileAsync(userId, profileDTO, baseUrl, env);
            return HandleResult(result);
        }
    }
}
