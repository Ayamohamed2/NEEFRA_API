using Microsoft.Extensions.Logging;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.DTO;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA.Domain.IReposatory;
using Restaurant.Core.Interfaces.IService.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.Services
{
    public class SmartRouteService :ISmartRouteService
    {
        private readonly IUnitOfWork unit;
        private readonly ILogger<SmartRouteService> _logger;
        private readonly IRedisCacheService _cache;



        public SmartRouteService(IUnitOfWork unit, ILogger<SmartRouteService> logger, IRedisCacheService cache)
        {
            this.unit = unit;
            _logger = logger;
            _cache = cache;
        }


        public async Task<ServiceResult<List<RouteArtPieceDTO>>> GetRouteAsync(
    string userId,
    string? groupId)
        {
            _logger.LogInformation(
                "GetRoute – userId:{UserId}, groupId:{GroupId}", userId, groupId);

            List<string> interestIds;

            if (!string.IsNullOrEmpty(groupId))
            {
                var group = await unit.Group.GetByFilterAsync(g => g.Id == groupId);
                if (group == null)
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Group not found",
                        ErrorType = "NotFound"
                    };

                

                // جيب الـ Interest IDs بتاعة الجروب
                var records = await unit.U_G_Interest
                    .GetAllAsync(ugi => ugi.GroupId == groupId);

                interestIds = records
                    .Select(ugi => ugi.Interest_Id)
                    .Distinct()
                    .ToList();
            }
            else
            {
                // جيب الـ Interest IDs بتاعة اليوزر اللوجين
                var records = await unit.U_G_Interest
                    .GetAllAsync(ugi => ugi.UserId == userId && ugi.GroupId == null);

                interestIds = records
                    .Select(ugi => ugi.Interest_Id)
                    .Distinct()
                    .ToList();
            }

            if (!interestIds.Any())
                return new()
                {
                    IsSuccess = true,
                    Message = "No interests found; returning empty route.",
                    Data = new List<RouteArtPieceDTO>()
                };

            var interestRecords = await unit.Interests
                .GetAllAsync(i => interestIds.Contains(i.Id));

            var interestNames = interestRecords
                .Select(i => i.Name)
                .ToHashSet();

            if (!interestNames.Any())
                return new()
                {
                    IsSuccess = true,
                    Message = "Interests could not be resolved.",
                    Data = new List<RouteArtPieceDTO>()
                };

            var validPieces = await unit.ArtPiece.GetAllAsync(p =>
                p.Floor != 0 &&
                p.Valid == true &&
                p.Latitude != 0 &&
                p.Longitude != 0);

            if (!validPieces.Any())
                return new() { IsSuccess = true, Data = new List<RouteArtPieceDTO>() };

        
            var allArtifacts = await unit.Artifcat.GetAllAsync(_ => true);

            var artifactDict = allArtifacts
                .GroupBy(a => a.Name)
                .ToDictionary(g => g.Key, g => g.First());

          
            var scored = validPieces
                .Select(piece =>
                {
                    int score = 0;

                    if (artifactDict.TryGetValue(piece.Name, out var artifact) &&
                        artifact.Interests != null)
                    {
                        // المقارنة صح: string (اسم) vs string (اسم)
                        score = artifact.Interests
                            .Count(i => interestNames.Contains(i));
                    }

                    return new { Piece = piece, Score = score };
                })
               .ToList();

            if (!scored.Any())
                return new() { IsSuccess = true, Data = new List<RouteArtPieceDTO>() };

      
            var deduped = scored
                .GroupBy(x => (x.Piece.Latitude, x.Piece.Longitude))
                .Select(g => g.OrderByDescending(x => x.Score).First())
                .ToList();

            // ── 7. Top 15 by Score ثم رتبهم بـ Lat → Lng ─────────────────────────
            var result = deduped
                .OrderByDescending(x => x.Score)   // الأعلى سكور الأول
                .Take(15)                           // أخد أحسن 15
                .OrderBy(x => x.Piece.Latitude)    // رتب بـ Latitude
                .ThenBy(x => x.Piece.Longitude)    // لو Lat متساوية، رتب بـ Longitude
                .Select(x => new RouteArtPieceDTO
                {
                    Name = x.Piece.Name,
                    Floor = x.Piece.Floor,
                    Valid = x.Piece.Valid,
                    Latitude = x.Piece.Latitude,
                    Longitude = x.Piece.Longitude,
                    Score = x.Score
                })
                .ToList();

            _logger.LogInformation("GetRoute – returning {Count} pieces", result.Count);
            return new() { IsSuccess = true, Data = result };
        }
    }
}
