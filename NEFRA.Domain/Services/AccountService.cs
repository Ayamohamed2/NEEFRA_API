using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using NEEFRA.Core.DTO.Account;
using NEEFRA.Core.DTO.Email;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Entities.Account;
using NEEFRA.Core;
using NEEFRA.Domain.IReposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;
using Google.Apis.Auth;
using Newtonsoft.Json.Linq;
using Hangfire;

namespace NEEFRA.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork unit;
        private readonly IAuthRepository auth;
        private readonly IEmailSender emailSender;
        private readonly IJWT_TokenReposatory jwt_tokens;
        private readonly ILogger<AccountService> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IBackgroundJobClient _jobClient;

        public AccountService(IBackgroundJobClient _jobClient, IConfiguration _config, IHttpClientFactory _httpClientFactory,IUnitOfWork unit, IAuthRepository auth, IEmailSender emailSender, IJWT_TokenReposatory jwt_tokens, ILogger<AccountService> logger)
        {
            this.unit = unit;
            this.auth = auth;
            this.emailSender = emailSender;
            this.jwt_tokens = jwt_tokens;
            _logger = logger;
            this._config = _config;
            this._httpClientFactory = _httpClientFactory;
            this._jobClient = _jobClient;
        }

        public async Task<ServiceResult<string>> RegisterAsync(RegisterDTO model, string origin)
        {
            _logger.LogInformation("Register attempt for email: {Email}", model.Email);

            var result = await auth.Register(model);
            if (result == null)
            {
                _logger.LogWarning("Registration failed - email already exists: {Email}", model.Email);
                return new() { IsSuccess = false, Message = "EmailAlreadyExists", ErrorType = "BadRequest" };
            }

            var user = await unit.Users.GetByFilterAsync(u => u.Email == model.Email);

            var token = Guid.NewGuid().ToString();

            await unit.EmailTokens.AddAsync(new EmailConfirmationToken
            {
                UserId = user.Id,
                Token = token,
                ExpireAt = DateTime.UtcNow.AddHours(24)
            });

            var link = $"{origin}/api/account/confirm-email?userId={user.Id}&token={token}";

        
            _jobClient.Enqueue<IEmailSender>(sender =>
        sender.SendEmailAsync(user.Email, "Confirm Email",
                $"<a href='{link}'>Confirm Email</a>"));
            _logger.LogInformation("User registered successfully, confirmation email sent to: {Email}", model.Email);
            return new() { IsSuccess = true, Message = "Registered successfully, please confirm your email." };
        }

        public async Task<ServiceResult<TokenDTO>> LoginAsync(LoginDTO model)
        {
            _logger.LogInformation("Login attempt for email: {Email}", model.Email);

            var result = await auth.LoginAsync(model);

            if (result.Message == "InvalidCredentials")
            {
                _logger.LogWarning("Login failed - invalid credentials for email: {Email}", model.Email);
                return new() { IsSuccess = false, Message = "Invalid email or password", ErrorType = "Unauthorized" };
            }

            if (result.Message == "EmailNotConfirmed")
            {
                _logger.LogWarning("Login failed - email not confirmed for: {Email}", model.Email);
                return new() { IsSuccess = false, Message = "Email not confirmed", ErrorType = "BadRequest" };
            }

            if (result.Message == "LockedOut")
            {
                _logger.LogWarning("Login failed - account locked out for: {Email}", model.Email);
                return new() { IsSuccess = false, Message = "Account locked. Please try again in 1 minute.", ErrorType = "BadRequest" };
            }

            _logger.LogInformation("User logged in successfully: {Email}", model.Email);
            return new() { IsSuccess = true, Data = result };
        }

        public async Task<ServiceResult<string>> SendEmailConfirmationAsync(EmailDTO email, string origin)
        {
            _logger.LogInformation("Sending confirmation email to: {Email}", email.Email);

            var user = await unit.Users.GetByFilterAsync(u => u.Email == email.Email);
            if (user == null)
            {
                _logger.LogWarning("Confirmation email failed - email not found: {Email}", email.Email);
                return new() { IsSuccess = false, Message = "Email not found", ErrorType = "NotFound" };
            }

            var token = Guid.NewGuid().ToString();

            await unit.EmailTokens.AddAsync(new EmailConfirmationToken
            {
                UserId = user.Id,
                Token = token,
                ExpireAt = DateTime.UtcNow.AddHours(24)
            });

            var link = $"{origin}/api/account/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(token)}";

           

            _jobClient.Enqueue<IEmailSender>(sender =>
    sender.SendEmailAsync(user.Email, "Confirm your email",
                $@"<h3>Email Confirmation</h3>
               <p>Please confirm your email by clicking the link below:</p>
               <a href='{link}'>Confirm Email</a>"));

            _logger.LogInformation("Confirmation email sent successfully to: {Email}", email.Email);
            return new() { IsSuccess = true, Message = "Email sent successfully. Please check your inbox." };
        }

        public async Task<ServiceResult<string>> ConfirmEmailAsync(string userId, string token)
        {
            _logger.LogInformation("Email confirmation attempt for userId: {UserId}", userId);

            var emailToken = await unit.EmailTokens.GetValidTokenAsync(userId, token);
            if (emailToken == null)
            {
                _logger.LogWarning("Email confirmation failed - invalid or expired token for userId: {UserId}", userId);
                return new() { IsSuccess = false, Message = "Invalid or expired token", ErrorType = "BadRequest" };
            }

            var user = await unit.Users.GetByFilterAsync(u => u.Id == userId);
            if (user == null)
            {
                _logger.LogWarning("Email confirmation failed - user not found: {UserId}", userId);
                return new() { IsSuccess = false, Message = "User not found", ErrorType = "NotFound" };
            }

            user.IsEmailConfirmed = true;
            await unit.Users.UpdateAsync(u => u.Id == userId, user);
            await unit.EmailTokens.DeleteAsync(emailToken.Id);

            _logger.LogInformation("Email confirmed successfully for userId: {UserId}", userId);
            return new() { IsSuccess = true, Message = "Email confirmed successfully" };
        }

        public async Task<ServiceResult<string>> ForgetPasswordAsync(EmailDTO model, string origin)
        {
            _logger.LogInformation("Forget password request for email: {Email}", model.Email);

            var user = await unit.Users.GetByFilterAsync(u => u.Email == model.Email);
            if (user == null)
            {
                _logger.LogWarning("Forget password failed - email not found: {Email}", model.Email);
                return new() { IsSuccess = false, Message = "Email not found", ErrorType = "NotFound" };
            }

            var token = Guid.NewGuid().ToString();

            await unit.PasswordResetTokens.AddAsync(new PasswordResetToken
            {
                UserId = user.Id,
                Token = token,
                ExpireAt = DateTime.UtcNow.AddMinutes(30)
            });

            var link = $"{origin}/api/account/reset-password?token={token}";

        
            _jobClient.Enqueue<IEmailSender>(sender =>
  sender.SendEmailAsync(user.Email, "Reset Password",
                $"<a href='{link}'>Reset Password</a>"));

            _logger.LogInformation("Password reset link sent to: {Email}", model.Email);
            return new() { IsSuccess = true, Message = "Reset password link sent" };
        }

        public async Task<ServiceResult<string>> ResetPasswordAsync(string token, ResetPasswordDTO model)
        {
            _logger.LogInformation("Password reset attempt with token");

            var resetToken = await unit.PasswordResetTokens.GetValidTokenAsync(token);
            if (resetToken == null)
            {
                _logger.LogWarning("Password reset failed - invalid or expired token");
                return new() { IsSuccess = false, Message = "Invalid or expired token", ErrorType = "BadRequest" };
            }

            var user = await unit.Users.GetByFilterAsync(u => u.Id == resetToken.UserId);
            if (user == null)
            {
                _logger.LogWarning("Password reset failed - user not found for userId: {UserId}", resetToken.UserId);
                return new() { IsSuccess = false, Message = "User not found", ErrorType = "NotFound" };
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            await unit.Users.UpdateAsync(u => u.Id == user.Id, user);
            await unit.PasswordResetTokens.DeleteAsync(resetToken.Id);

            _logger.LogInformation("Password reset successfully for userId: {UserId}", user.Id);
            return new() { IsSuccess = true, Message = "Password reset successfully" };
        }

        public async Task<ServiceResult<string>> ChangePasswordAsync(string userId, ChangePasswordDTO model)
        {
            _logger.LogInformation("Change password request for userId: {UserId}", userId);

            var user = await unit.Users.GetByFilterAsync(u => u.Id == userId);
            if (user == null)
            {
                _logger.LogWarning("Change password failed - user not found: {UserId}", userId);
                return new() { IsSuccess = false, Message = "Unauthorized", ErrorType = "Unauthorized" };
            }

            if (!BCrypt.Net.BCrypt.Verify(model.CurrentPassword, user.PasswordHash))
            {
                _logger.LogWarning("Change password failed - incorrect current password for userId: {UserId}", userId);
                return new() { IsSuccess = false, Message = "Current password is incorrect", ErrorType = "BadRequest" };
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            await unit.Users.UpdateAsync(u => u.Id == user.Id, user);

            _logger.LogInformation("Password changed successfully for userId: {UserId}", userId);
            return new() { IsSuccess = true, Message = "Password changed successfully" };
        }

        public async Task<ServiceResult<string>> ChangeEmailAsync(string userId, EmailDTO model)
        {
            _logger.LogInformation("Change email request for userId: {UserId}", userId);

            var user = await unit.Users.GetByFilterAsync(u => u.Id == userId);
            if (user == null)
            {
                _logger.LogWarning("Change email failed - user not found: {UserId}", userId);
                return new() { IsSuccess = false, Message = "Unauthorized", ErrorType = "Unauthorized" };
            }

            var exist = await unit.Users.GetAllAsync(u => u.Email == model.Email);
            if (exist.Any())
            {
                _logger.LogWarning("Change email failed - email already exists: {NewEmail}", model.Email);
                return new() { IsSuccess = false, Message = "EmailExists", ErrorType = "BadRequest" };
            }

            user.Email = model.Email;
            user.IsEmailConfirmed = false;

            await unit.Users.UpdateAsync(u => u.Id == user.Id, user);

            _logger.LogInformation("Email updated for userId: {UserId}, new email: {NewEmail}", userId, model.Email);
            return new() { IsSuccess = true, Message = "Email updated, please confirm it again" };
        }

        public async Task<ServiceResult<TokenDTO>> RefreshTokenAsync(TokenDTO tokenDTO)
        {
            _logger.LogInformation("Refresh token attempt");

            var result = await jwt_tokens.RefreshAccessToken(tokenDTO);

            if (result == null || string.IsNullOrEmpty(result.AccessToken))
            {
                _logger.LogWarning("Refresh token failed - invalid token");
                return new ServiceResult<TokenDTO>
                {
                    IsSuccess = false,
                    Message = "Invalid Token",
                    ErrorType = "BadRequest"
                };
            }

            _logger.LogInformation("Token refreshed successfully");
            return new ServiceResult<TokenDTO>
            {
                IsSuccess = true,
                Data = result
            };
        }

        public async Task<ServiceResult<string>> LogoutAsync(TokenDTO tokenDTO)
        {
            _logger.LogInformation("Logout attempt");

            var existingRefreshToken =
                await unit.RefreshToken.GetByFilterAsync(r => r.Refresh_Token == tokenDTO.RefreshToken);

            if (existingRefreshToken == null)
            {
                _logger.LogWarning("Logout failed - invalid refresh token");
                return new ServiceResult<string>
                {
                    IsSuccess = false,
                    Message = "Invalid Token",
                    ErrorType = "BadRequest"
                };
            }

            await jwt_tokens.RevokeAllTokens(tokenDTO);

            _logger.LogInformation("User logged out successfully");
            return new ServiceResult<string>
            {
                IsSuccess = true,
                Message = "Logged out successfully"
            };
        }

        //=========================================================================
        public async Task<ServiceResult<TokenDTO>> GoogleLoginExternalAsync(string email, string name)
        {
            _logger.LogInformation("Google external login for {Email}", email);

            var user = await unit.Users.GetByFilterAsync(u => u.Email == email);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = email,
                    Name = name,
                    IsEmailConfirmed = true
                };

                await unit.Users.CreateAsync(user);
            }
            string jwtTokenId = $"JTI{Guid.NewGuid()}";
            var accessToken = await jwt_tokens.GenerateToken(user, jwtTokenId);
            var refreshToken = await jwt_tokens.CreateNewRefreshToken(user.Id, jwtTokenId);


            var token = new TokenDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };


            return new ServiceResult<TokenDTO>
            {
                IsSuccess = true,
                Data = token
            };
        }


    }
}
