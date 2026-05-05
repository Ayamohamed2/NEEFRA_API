using Microsoft.Extensions.Logging;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.DTO.Service.Summary;
using NEEFRA.Core.Enum;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA.Domain.IReposatory;
using NEEFRA_API.Models;
using Restaurant.Core.Interfaces.IService.Redis;
using System.Text.Json;

namespace NEEFRA.Core.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly IUnitOfWork unit;
        private readonly ILogger<SummaryService> _logger;
        private readonly IRedisCacheService _cache;

        // Cache keys
        private const string FULL_SUMMARY_KEY = "summary:visit:{0}";           // {0} = visitId
        private const string USER_SUMMARY_KEY = "summary:visit:{0}:user:{1}";  // {0} = visitId, {1} = userId

        // Cache duration
        private static readonly TimeSpan SummaryExpiry = TimeSpan.FromMinutes(30);

        // ── الـ JSON options نفس اللي في RedisCacheService ──────────────────
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public SummaryService(IUnitOfWork unit, ILogger<SummaryService> logger, IRedisCacheService cache)
        {
            this.unit = unit;
            _logger = logger;
            _cache = cache;
        }

        // ─────────────────────────────────────────────
        // Summary (full visit)
        // ─────────────────────────────────────────────
        public async Task<SummaryDTo<List<object>>> Summary(string visitId, string baseurl)
        {
            _logger.LogInformation("Fetching full summary for visitId: {VisitId}", visitId);

            if (string.IsNullOrEmpty(visitId))
            {
                _logger.LogWarning("Summary failed – visitId is empty");
                return new() { IsSuccess = false, Message = "Visit Id is empty", ErrorType = "BadRequest" };
            }

            var cacheKey = string.Format(FULL_SUMMARY_KEY, visitId);
            var fromCache = await TryGetFromCacheAsync(cacheKey);
            if (fromCache is not null)
            {
                _logger.LogDebug("Cache hit – full summary for visitId: {VisitId}", visitId);
                return fromCache;
            }

            var summary = await BuildSummaryAsync(visitId, baseurl, userFilter: null);

            if (summary.IsSuccess)
                await SaveToCacheAsync(cacheKey, summary);

            return summary;
        }

        // ─────────────────────────────────────────────
        // SummaryForUser
        // ─────────────────────────────────────────────
        public async Task<SummaryDTo<List<object>>> SummaryForUser(string visitId, string baseurl, string UserId)
        {
            _logger.LogInformation("Fetching summary for visitId: {VisitId}, userId: {UserId}", visitId, UserId);

            if (string.IsNullOrEmpty(visitId))
            {
                _logger.LogWarning("SummaryForUser failed – visitId is empty");
                return new() { IsSuccess = false, Message = "Visit Id is empty", ErrorType = "BadRequest" };
            }

            if (string.IsNullOrEmpty(UserId))
            {
                _logger.LogWarning("SummaryForUser failed – userId is empty");
                return new() { IsSuccess = false, Message = "User Id is empty", ErrorType = "BadRequest" };
            }

            var cacheKey = string.Format(USER_SUMMARY_KEY, visitId, UserId);
            var fromCache = await TryGetFromCacheAsync(cacheKey);
            if (fromCache is not null)
            {
                _logger.LogDebug("Cache hit – user summary for visitId: {VisitId}, userId: {UserId}", visitId, UserId);
                return fromCache;
            }

            var summary = await BuildSummaryAsync(visitId, baseurl, userFilter: UserId);

            if (summary.IsSuccess)
                await SaveToCacheAsync(cacheKey, summary);

            return summary;
        }

        // ─────────────────────────────────────────────
        // Shared builder – eliminates duplicated logic
        // ─────────────────────────────────────────────
        private async Task<SummaryDTo<List<object>>> BuildSummaryAsync(
            string visitId, string baseurl, string? userFilter)
        {
            var piecesInRoute = userFilter is null
                ? await unit.RoutePiece.GetAllAsync(p => p.VisitId == visitId)
                : await unit.RoutePiece.GetAllAsync(p => p.VisitId == visitId && p.UserId == userFilter);

            _logger.LogInformation("Found {Count} route pieces for visitId: {VisitId}", piecesInRoute.Count, visitId);

            var pieceIds = piecesInRoute.Select(p => p.PieceId).ToList();
            var pieces = await unit.ArtPiece.GetAllAsync(p => pieceIds.Contains(p.Id));
            var pieceDict = pieces.ToDictionary(p => p.Id, p => p);

            var userIds = piecesInRoute.Select(p => p.UserId).ToList();
            var users = await unit.Users.GetAllAsync(u => userIds.Contains(u.Id));
            var userDict = users.ToDictionary(u => u.Id, u => u);

            var interactions = await unit.Favourite.GetAllAsync(f => f.VisitId == visitId);
            var interactedUserIds = interactions.Select(u => u.UserId);
            var interactedUsers = await unit.Users.GetAllAsync(u => interactedUserIds.Contains(u.Id));
            var interactedUsersDict = interactedUsers.ToDictionary(u => u.Id, u => u);

            var result = piecesInRoute.Select(p =>
            {
                var allInteractions = interactions.Where(i => i.ArtPieceId == p.PieceId);

                pieceDict.TryGetValue(p.PieceId, out var piece);
                userDict.TryGetValue(p.UserId, out var user);

                var usersInteraction = allInteractions.Select(i =>
                {
                    interactedUsersDict.TryGetValue(i.UserId, out var interactedUser);
                    return new
                    {
                        UserId = i.UserId,
                        UserName = interactedUser?.Name,
                        Img = string.IsNullOrEmpty(interactedUser?.ImageURL)
                                   ? null
                                   : baseurl + interactedUser.ImageURL
                    };
                });

                return (object)new
                {
                    p.VisitId,
                    ViewerId = p.UserId,
                    ViewerName = user?.Name,
                    Viewerimg = string.IsNullOrEmpty(user?.ImageURL) ? null : baseurl + user.ImageURL,
                    p.PieceId,
                    PieceName = piece?.Name,
                    PieceImg = string.IsNullOrEmpty(p.ImageURl) ? null : baseurl + p.ImageURl,
                    p.VisitAt,
                    p.VisitOrder,
                    p.Visited,
                    UsersInteraction = usersInteraction
                };
            }).ToList();

            var visit = await unit.Visit.GetByFilterAsync(v => v.Id == visitId);
            var museumName = (await unit.Museum.GetByIdAsync(visit?.MuseumId))?.Name;

            _logger.LogInformation("Summary built for visitId: {VisitId}, museum: {MuseumName}", visitId, museumName);

            return new()
            {
                IsSuccess = true,
                MuseumName = museumName,
                VisitType = visit?.VisitType == VisitType.Solo ? "Solo" : "Group",
                VisitStartTime = visit?.StartTime,
                VisitEndTime = visit?.EndTime,
                IsInsideMuseum = visit?.IsInsideMuseum ?? false,
                Data = result
            };
        }

        // ─────────────────────────────────────────────
        // Public helper – call from VisitService when
        // the visit ends to bust all related cache
        // ─────────────────────────────────────────────
        public async Task InvalidateSummaryCacheAsync(string visitId)
        {
            _logger.LogInformation("Invalidating summary cache for visitId: {VisitId}", visitId);
            await _cache.RemoveByPatternAsync($"summary:visit:{visitId}*");
        }

        // ═══════════════════════════════════════════════════════════════════
        // Cache helpers
        //
        // المشكلة الأصلية:
        //   الـ Data بتاعة الـ SummaryDTo هي List<object> مبنية من
        //   anonymous types — الـ JSON serializer بيحولها لـ JSON صح،
        //   لكن لما بيرجع يـdeserialize بيرجع List<JsonElement> مش
        //   List<object> حقيقية، فالـ Data بتبان مش null لكن الـ
        //   controller لما بيبعتها للـ client بتبقى فاضية أو غلط.
        //
        // الحل:
        //   نخزن الـ Data كـ raw JSON string منفصل، ونخزن باقي الـ
        //   envelope fields (MuseumName, VisitType...) كـ wrapper بسيط
        //   بيتـdeserialize صح لأن fields مش anonymous types.
        // ═══════════════════════════════════════════════════════════════════

        private async Task SaveToCacheAsync(string key, SummaryDTo<List<object>> summary)
        {
            try
            {
                // نحول الـ Data لـ raw JSON string عشان نحتفظ بشكله الأصلي
                var rawDataJson = JsonSerializer.Serialize(summary.Data, _jsonOptions);

                var wrapper = new SummaryCacheWrapper
                {
                    MuseumName = summary.MuseumName,
                    VisitType = summary.VisitType,
                    VisitStartTime = summary.VisitStartTime,
                    VisitEndTime = summary.VisitEndTime,
                    IsInsideMuseum = summary.IsInsideMuseum,
                    RawDataJson = rawDataJson
                };

                await _cache.SetAsync(key, wrapper, SummaryExpiry);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to cache summary for key: {Key}", key);
            }
        }

        private async Task<SummaryDTo<List<object>>?> TryGetFromCacheAsync(string key)
        {
            try
            {
                var wrapper = await _cache.GetAsync<SummaryCacheWrapper>(key);
                if (wrapper is null) return null;

                // نرجع الـ Data من الـ raw JSON — هيرجع كـ List<JsonElement>
                // وده كافي لأن الـ controller هيـserializeه تاني للـ client
                var data = JsonSerializer.Deserialize<List<object>>(wrapper.RawDataJson, _jsonOptions);
                if (data is null) return null;

                return new SummaryDTo<List<object>>
                {
                    IsSuccess = true,
                    MuseumName = wrapper.MuseumName,
                    VisitType = wrapper.VisitType,
                    VisitStartTime = wrapper.VisitStartTime,
                    VisitEndTime = wrapper.VisitEndTime,
                    IsInsideMuseum = wrapper.IsInsideMuseum,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to read summary from cache for key: {Key}", key);
                return null;
            }
        }

        // DTO داخلي للـ cache بس — مش بيتكشف للخارج
        private sealed class SummaryCacheWrapper
        {
            public string? MuseumName { get; set; }
            public string? VisitType { get; set; }
            public DateTime? VisitStartTime { get; set; }
            public DateTime? VisitEndTime { get; set; }
            public bool IsInsideMuseum { get; set; }
            public string RawDataJson { get; set; } = "[]";
        }
    }
}