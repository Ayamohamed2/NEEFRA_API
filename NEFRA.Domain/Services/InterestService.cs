using Microsoft.Extensions.Logging;
using NEEFRA.Core.DTO.Interests;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Entities.Inerests;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA.Domain.IReposatory;
using Restaurant.Core.Interfaces.IService.Redis;

namespace NEEFRA.Core.Services
{
    public class InterestService : IInterestService
    {
        private readonly IUnitOfWork unit;
        private readonly ILogger<InterestService> _logger;
        private readonly IRedisCacheService _cache;

        // Cache keys
        private const string ALL_INTERESTS_KEY   = "interests:all";
        private const string USER_INTERESTS_KEY  = "interests:user:{0}";    // {0} = userId
        private const string GROUP_INTERESTS_KEY = "interests:group:{0}";   // {0} = groupId

        // Cache durations
        private static readonly TimeSpan AllInterestsExpiry  = TimeSpan.FromHours(6);   // static data → long TTL
        private static readonly TimeSpan UserGroupExpiry     = TimeSpan.FromMinutes(30);

        public InterestService(IUnitOfWork unit, ILogger<InterestService> logger, IRedisCacheService cache)
        {
            this.unit = unit;
            _logger   = logger;
            _cache    = cache;
        }

        // ─────────────────────────────────────────────
        // GetIntersets
        // ─────────────────────────────────────────────
        public async Task<ServiceResult<List<Interest>>> GetIntersets()
        {
            _logger.LogInformation("Fetching all interests");

            var cached = await _cache.GetAsync<List<Interest>>(ALL_INTERESTS_KEY);
            if (cached is not null)
            {
                _logger.LogDebug("Cache hit – all interests ({Count})", cached.Count);
                return new ServiceResult<List<Interest>> { IsSuccess = true, Data = cached };
            }

            var result = await unit?.Interests?.GetAllAsync();
            _logger.LogInformation("Fetched {Count} interests from DB", result?.Count ?? 0);

            if (result is not null)
                await _cache.SetAsync(ALL_INTERESTS_KEY, result, AllInterestsExpiry);

            return new ServiceResult<List<Interest>> { IsSuccess = true, Data = result };
        }

        // ─────────────────────────────────────────────
        // Get_U_G_Interests
        // ─────────────────────────────────────────────
        public async Task<ServiceResult<List<object>>> Get_U_G_Interests(string? GroupId, string? userId)
        {
            _logger.LogInformation("Fetching interests – GroupId: {GroupId}, UserId: {UserId}", GroupId, userId);

            var cacheKey = string.IsNullOrEmpty(GroupId)
                ? string.Format(USER_INTERESTS_KEY, userId)
                : string.Format(GROUP_INTERESTS_KEY, GroupId);

            var cached = await _cache.GetAsync<List<object>>(cacheKey);
            if (cached is not null)
            {
                _logger.LogDebug("Cache hit – {Key}", cacheKey);
                return new ServiceResult<List<object>> { IsSuccess = true, Data = cached };
            }

            List<User_group_Interest> result;
            if (string.IsNullOrEmpty(GroupId))
                result = await unit.U_G_Interest.GetAllAsync(i => i.UserId == userId);
            else
                result = await unit.U_G_Interest.GetAllAsync(i => i.GroupId == GroupId);

            var interestIds  = result.Select(i => i.Interest_Id).ToList();
            var interestsDict = (await unit.Interests.GetAllAsync(i => interestIds.Contains(i.Id)))
                                .ToDictionary(i => i.Id, i => i.Name);

            var response = result.Select(i =>
            {
                interestsDict.TryGetValue(i.Interest_Id, out var name);
                return (object)new { Interest_Id = i.Interest_Id, InterestName = name };
            }).ToList();

            _logger.LogInformation("Fetched {Count} interests for GroupId: {GroupId}, UserId: {UserId}",
                response.Count, GroupId, userId);

            await _cache.SetAsync(cacheKey, response, UserGroupExpiry);

            return new ServiceResult<List<object>> { IsSuccess = true, Data = response };
        }

        // ─────────────────────────────────────────────
        // AddInterst  – invalidate related cache
        // ─────────────────────────────────────────────
        public async Task<ServiceResult<object>> AddInterst(U_G_InterestDTO dto, string? userId)
        {
            _logger.LogInformation("Adding interest – InterestId: {InterestId}, GroupId: {GroupId}, UserId: {UserId}",
                dto.InterestId, dto.GroupId, userId);

            if ((string.IsNullOrEmpty(dto.GroupId) && string.IsNullOrEmpty(userId)) ||
                string.IsNullOrEmpty(dto.InterestId))
            {
                _logger.LogWarning("Add interest failed – invalid input");
                return new() { IsSuccess = false, Message = "Invalid Input", ErrorType = "BadRequest" };
            }

            var entity = new User_group_Interest
            {
                UserId      = dto.GroupId == null ? userId : null,
                GroupId     = dto.GroupId,
                Interest_Id = dto.InterestId
            };
            await unit.U_G_Interest.CreateAsync(entity);

            // Invalidate only the affected cache entry
            await InvalidateInterestCache(dto.GroupId, userId);

            _logger.LogInformation("Interest added successfully – InterestId: {InterestId}", dto.InterestId);
            return new ServiceResult<object> { IsSuccess = true, Data = entity };
        }

        // ─────────────────────────────────────────────
        // DeleteInterst  – invalidate related cache
        // ─────────────────────────────────────────────
        public async Task<ServiceResult<object>> DeleteInterst(U_G_InterestDTO dto, string? userId)
        {
            _logger.LogInformation("Deleting interest – InterestId: {InterestId}, GroupId: {GroupId}, UserId: {UserId}",
                dto.InterestId, dto.GroupId, userId);

            if ((string.IsNullOrEmpty(dto.GroupId) && string.IsNullOrEmpty(userId)) ||
                string.IsNullOrEmpty(dto.InterestId))
            {
                _logger.LogWarning("Delete interest failed – invalid input");
                return new() { IsSuccess = false, Message = "Delete Fails", ErrorType = "BadRequest" };
            }

            User_group_Interest entity;
            if (string.IsNullOrEmpty(dto.GroupId))
                entity = await unit.U_G_Interest.GetByFilterAsync(i => i.Interest_Id == dto.InterestId && i.UserId == userId);
            else
                entity = await unit.U_G_Interest.GetByFilterAsync(i => i.Interest_Id == dto.InterestId && i.GroupId == dto.GroupId);

            if (entity == null)
            {
                _logger.LogWarning("Delete interest failed – not found");
                return new() { IsSuccess = false, Message = "Delete Fails", ErrorType = "BadRequest" };
            }

            await unit.U_G_Interest.DeleteAsync(i => i.Id == entity.Id);

            await InvalidateInterestCache(dto.GroupId, userId);

            _logger.LogInformation("Interest deleted successfully – InterestId: {InterestId}", dto.InterestId);
            return new() { IsSuccess = true, Data = entity };
        }

        // ─────────────────────────────────────────────
        // Helper
        // ─────────────────────────────────────────────
        private async Task InvalidateInterestCache(string? groupId, string? userId)
        {
            if (!string.IsNullOrEmpty(groupId))
                await _cache.RemoveAsync(string.Format(GROUP_INTERESTS_KEY, groupId));
            else if (!string.IsNullOrEmpty(userId))
                await _cache.RemoveAsync(string.Format(USER_INTERESTS_KEY, userId));
        }
    }
}