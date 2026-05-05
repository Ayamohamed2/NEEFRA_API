using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NEEFRA.Core.DTO.Profile;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Entities.Account;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA.Domain.IReposatory;
using Org.BouncyCastle.Asn1.Ocsp;
using Restaurant.Core.Interfaces.IService.Redis;


namespace NEEFRA.Core.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork unit;
        private readonly ILogger<UserProfileService> _logger;
        private readonly IRedisCacheService cache;

        public UserProfileService(IUnitOfWork unit, ILogger<UserProfileService> logger,IRedisCacheService cache)
        {
            this.unit = unit;
            _logger = logger;
            this.cache = cache;
        }

        public async Task<ServiceResult<UserProfileResponseDTO>> GetProfileAsync(string userId, string baseUrl)
        {
            _logger.LogInformation("Fetching profile for userId: {UserId}", userId);

            var cachekey = $"Profile:{userId}";
            ApplicationUser user = await cache.GetAsync<ApplicationUser>(cachekey);
            if (user == null)
            {
                user = await unit.Users.GetByFilterAsync(u=>u.Id==userId);
                var profile = new
                {
                    user.Email,
                    user.Name,
                    user.ImageURL
                };
                await cache.SetAsync(cachekey, profile, TimeSpan.FromMinutes(30));
            }

            if (user == null)
            {
                _logger.LogWarning("Get profile failed - user not found: {UserId}", userId);
                return new() { IsSuccess = false, Message = "User not found", ErrorType = "BadRequest" };
            }

            var imageUrl = string.IsNullOrEmpty(user.ImageURL)
                ? null
                : baseUrl + user.ImageURL;

            _logger.LogInformation("Profile fetched successfully for userId: {UserId}", userId);
            return new()
            {
                IsSuccess = true,
                Data = new UserProfileResponseDTO
                {
                    Email = user.Email,
                    Name = user.Name,
                    ImageUrl = imageUrl
                }
            };
        }

        public async Task<ServiceResult<UserProfileResponseDTO>> UpdateProfileAsync(string userId, UserProfileDTO profileDTO, string baseUrl, IWebHostEnvironment env)
        {
            _logger.LogInformation("Updating profile for userId: {UserId}", userId);

            var user = await unit.Users.GetByFilterAsync(u => u.Id == userId);

            if (user == null)
            {
                _logger.LogWarning("Update profile failed - user not found: {UserId}", userId);
                return new() { IsSuccess = false, Message = "User not found", ErrorType = "NotFound" };
            }

            if (profileDTO.Name != null)
            {
                _logger.LogInformation("Updating name for userId: {UserId}", userId);
                user.Name = profileDTO.Name;
            }

            if (profileDTO.imagefile != null && profileDTO.imagefile.Length > 0)
            {
                _logger.LogInformation("Updating profile image for userId: {UserId}", userId);

                if (user.ImageURL != "/Images/default.png")
                    unit.Users.DeleteImageMethod(user.ImageURL, env);

                user.ImageURL = unit.Users.GetImageURL(profileDTO.imagefile, user.Id, env);
            }

            await unit.Users.UpdateAsync(u => u.Id == userId, user);

            var imageUrl = string.IsNullOrEmpty(user.ImageURL)
                ? null
                : baseUrl + user.ImageURL;
            

            _logger.LogInformation("Profile updated successfully for userId: {UserId}", userId);
            await cache.RemoveAsync($"Profile:{userId}");
            return new()
            {
                IsSuccess = true,
                Data = new UserProfileResponseDTO
                {
                    Email = user.Email,
                    Name = user.Name,
                    ImageUrl = imageUrl
                }
            };
        }
    }
}