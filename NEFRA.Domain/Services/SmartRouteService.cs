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
    public class SmartRouteService : ISmartRouteService
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

        // ─────────────────────────────────────────────────────────────────────
        // Haversine distance in meters between two lat/lng points
        // ─────────────────────────────────────────────────────────────────────
        private static double HaversineMeters(double lat1, double lng1, double lat2, double lng2)
        {
            const double R = 6_371_000;
            double dLat = (lat2 - lat1) * Math.PI / 180.0;
            double dLng = (lng2 - lng1) * Math.PI / 180.0;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                     + Math.Cos(lat1 * Math.PI / 180.0)
                     * Math.Cos(lat2 * Math.PI / 180.0)
                     * Math.Sin(dLng / 2) * Math.Sin(dLng / 2);

            return R * 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        }

        // ─────────────────────────────────────────────────────────────────────
        // Nearest-Neighbor TSP greedy algorithm
        // Starts from the user's current location, then at each step picks the
        // closest unvisited piece by Haversine distance. O(n²) — fine for n≤15.
        // ─────────────────────────────────────────────────────────────────────
        private static List<PathPointDTO> NearestNeighborPath(
            List<PathPointDTO> pieces,
            double startLat,
            double startLng)
        {
            var unvisited = new List<PathPointDTO>(pieces);
            var path = new List<PathPointDTO>(pieces.Count);

            double curLat = startLat;
            double curLng = startLng;

            while (unvisited.Count > 0)
            {
                PathPointDTO nearest = unvisited[0];
                double nearestDist = HaversineMeters(curLat, curLng,
                                                      nearest.Latitude,
                                                      nearest.Longitude);

                for (int i = 1; i < unvisited.Count; i++)
                {
                    double d = HaversineMeters(curLat, curLng,
                                               unvisited[i].Latitude,
                                               unvisited[i].Longitude);
                    if (d < nearestDist)
                    {
                        nearestDist = d;
                        nearest = unvisited[i];
                    }
                }

                nearest.DistanceFromPrevMeters = Math.Round(nearestDist, 1);
                path.Add(nearest);
                unvisited.Remove(nearest);

                curLat = nearest.Latitude;
                curLng = nearest.Longitude;
            }

            return path;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Main method
        // ─────────────────────────────────────────────────────────────────────
        public async Task<ServiceResult<RouteResultDTO>> GetRouteAsync(
            string userId,
            string? groupId,
            double userLat,
            double userLng)
        {
            _logger.LogInformation(
                "GetRoute – userId:{UserId}, groupId:{GroupId}, start:({Lat},{Lng})",
                userId, groupId, userLat, userLng);

            // ── 1. Resolve Interest IDs ───────────────────────────────────────
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

                var records = await unit.U_G_Interest
                    .GetAllAsync(ugi => ugi.GroupId == groupId);

                interestIds = records
                    .Select(ugi => ugi.Interest_Id)
                    .Distinct()
                    .ToList();
            }
            else
            {
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
                    Data = new RouteResultDTO()
                };

            // ── 2. Resolve Interest Names ─────────────────────────────────────
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
                    Data = new RouteResultDTO()
                };

            // ── 3. Load Valid Art Pieces ──────────────────────────────────────
            var validPieces = await unit.ArtPiece.GetAllAsync(p =>
                p.Floor != 0 &&
                p.Valid == true &&
                p.Latitude != 0 &&
                p.Longitude != 0);

            if (!validPieces.Any())
                return new() { IsSuccess = true, Data = new RouteResultDTO() };

            // ── 4. Score each piece by how many interests it matches ──────────
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
                        score = artifact.Interests
                            .Count(i => interestNames.Contains(i));
                    }
                    return new { Piece = piece, Score = score };
                })
                .ToList();

            // ── 5. Remove duplicate coordinates (keep highest score per spot) ─
            var deduped = scored
                .GroupBy(x => (x.Piece.Latitude, x.Piece.Longitude))
                .Select(g => g.OrderByDescending(x => x.Score).First())
                .ToList();

            // ── 6. Top 15 by Score → build PathPointDTO list ─────────────────
            var candidates = deduped
                .OrderByDescending(x => x.Score)
                .Take(15)
                .Select(x => new PathPointDTO
                {
                    Type = "piece",
                    Name = x.Piece.Name,
                    Floor = x.Piece.Floor,
                    Latitude = x.Piece.Latitude,
                    Longitude = x.Piece.Longitude,
                    Valid=x.Piece.Valid,
                    Score = x.Score
                })
                .ToList();

            // ── 7. Nearest-Neighbor TSP → shortest ordered path ───────────────
            var orderedPieces = NearestNeighborPath(candidates, userLat, userLng);

            // ── 8. Build merged path (start point + pieces) ───────────────────
            var path = new List<PathPointDTO>();

            // نقطة البداية = موقع الـ user
            path.Add(new PathPointDTO
            {
                Order = 0,
                Type = "start",
                Name = "your Location",
                Floor = null,
                Latitude = userLat,
                Longitude = userLng,
                Score = null,
                DistanceFromPrevMeters = 0
            });

            // القطع بالترتيب
            path.AddRange(orderedPieces.Select((p, index) =>
            {
                p.Order = index + 1;
                return p;
            }));

            // ── 9. Total distance ─────────────────────────────────────────────
            double totalMeters = orderedPieces.Sum(p => p.DistanceFromPrevMeters);

            var result = new RouteResultDTO
            {
                Path = path,
                TotalDistanceMeters = Math.Round(totalMeters, 1)
            };

            _logger.LogInformation(
                "GetRoute – {Count} pieces, total distance {Dist}m",
                orderedPieces.Count, totalMeters);

            return new() { IsSuccess = true, Data = result };
        }
    }
}
