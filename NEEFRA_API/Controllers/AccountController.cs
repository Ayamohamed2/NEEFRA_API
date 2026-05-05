using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NEEFRA.API.Helpers;
using NEEFRA.Core.DTO.Account;
using NEEFRA.Core.DTO.Email;
using NEEFRA.Core.DTO.Service;
using System.Security.Claims;
using NEEFRA.Core;
using Restaurant.API.Controllers;
using Microsoft.AspNetCore.Authentication;
namespace Villa_API_Project.Controllers.V2
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

       

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            var origin = $"{Request.Scheme}://{Request.Host}";
            var result = await accountService.RegisterAsync(model, origin);
            return HandleResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var result = await accountService.LoginAsync(model);
            return HandleResult(result);
        }

        [HttpPost("email-confirmation")]
        public async Task<IActionResult> EmailForConfirmation(EmailDTO email)
        {
            var origin = $"{Request.Scheme}://{Request.Host}";
            var result = await accountService.SendEmailConfirmationAsync(email, origin);
            return HandleResult(result);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await accountService.ConfirmEmailAsync(userId, token);
            return HandleResult(result);
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword(EmailDTO model)
        {
            var origin = $"{Request.Scheme}://{Request.Host}";
            var result = await accountService.ForgetPasswordAsync(model, origin);
            return HandleResult(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, ResetPasswordDTO model)
        {
            var result = await accountService.ResetPasswordAsync(token, model);
            return HandleResult(result);
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await accountService.ChangePasswordAsync(userId, model);
            return HandleResult(result);
        }

        [Authorize]
        [HttpPost("change-email")]
        public async Task<IActionResult> ChangeEmail(EmailDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await accountService.ChangeEmailAsync(userId, model);
            return HandleResult(result);
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> GetNewTokenFromRefreshToken([FromBody] TokenDTO tokenDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Input");

            var result = await accountService.RefreshTokenAsync(tokenDTO);



            return HandleResult(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> RevokeRefreshTokenandAccessToken([FromBody] TokenDTO tokenDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Input");

            var result = await accountService.LogoutAsync(tokenDTO);



            return HandleResult(result);
        }
        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var redirectUrl = Url.Action("GoogleResponse", "Account", null, Request.Scheme);

            var properties = new AuthenticationProperties
            {
                RedirectUri = redirectUrl
            };

            return Challenge(properties, "Google");
        }
        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync("ExternalCookies");

            if (!result.Succeeded)
                return BadRequest("Google authentication failed");

            var email = result.Principal.FindFirstValue(ClaimTypes.Email);
            var name = result.Principal.FindFirstValue(ClaimTypes.Name);

            var serviceResult = await accountService.GoogleLoginExternalAsync(email, name);

            return HandleResult(serviceResult);
        }

    }
}
