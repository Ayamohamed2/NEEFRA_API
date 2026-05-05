using Microsoft.Extensions.Logging;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using NEEFRA_API.DTO;
using NEEFRA_API.Models;

namespace NEEFRA.Core.Services
{
    public class VisitService : IVisitService
    {
        private readonly IVisitRepository _visitRepo;
        private readonly IMuseumRepository _museumRepo;
        private readonly ILogger<VisitService> _logger;

        private const double AllowedRadiusInMeters = 100.0;

        public VisitService(IVisitRepository visitRepo, IMuseumRepository museumRepo, ILogger<VisitService> logger)
        {
            _visitRepo = visitRepo;
            _museumRepo = museumRepo;
            _logger = logger;
        }

        // ════════════════════════════════════════════════════════════════════
        // Visit Operations
        // ════════════════════════════════════════════════════════════════════

        public async Task<ServiceResult<object>> CheckAndStartAsync(string userId, CheckLocationDTO dto)
        {
            _logger.LogInformation("CheckAndStart – userId: {UserId}, museumId: {MuseumId}", userId, dto.MuseumId);

            var museum = await _museumRepo.GetByIdAsync(dto.MuseumId);
            if (museum == null)
            {
                _logger.LogWarning("CheckAndStart failed – museum not found: {MuseumId}", dto.MuseumId);
                return new() { IsSuccess = false, Message = "Museum not found", ErrorType = "NotFound" };
            }

            bool isInside = IsUserInsideMuseum(
                dto.Latitude, dto.Longitude,
                museum.Latitude, museum.Longitude
            );

            if (!isInside)
            {
                _logger.LogInformation("User is outside museum – userId: {UserId}, museumId: {MuseumId}", userId, dto.MuseumId);

                return new()
                {
                    IsSuccess = true,
                    Data = new
                    {
                        isInsideMuseum = false,
                        message = "You are outside the museum",
                        museum.Name,
                        museum.Location,
                        museum.GeneralInfo,
                        museum.Open_Hours,
                        museum.TicketEgyptAdultPrice,
                        museum.TicketEgyptStudentPrice,
                        museum.TicketForienerAdultPrice,
                        museum.TicketForienerStudentPrice
                    }
                };
            }

            var existingVisit = await _visitRepo.GetActiveVisitAsync(userId, dto.MuseumId);
            if (existingVisit != null)
            {
                _logger.LogWarning("CheckAndStart – active visit already exists – userId: {UserId}, museumId: {MuseumId}, visitId: {VisitId}",
                    userId, dto.MuseumId, existingVisit.Id);

                return new()
                {
                    IsSuccess = false,
                    Message = "An active visit already exists for this museum",
                    ErrorType = "Conflict",
                    Data = new
                    {
                        isInsideMuseum = true,
                        message = "An active visit already exists for this museum",
                        visitId = existingVisit.Id
                    }
                };
            }

            var visitType = dto.VisitType?.ToLower() == "group" ? VisitType.Group : VisitType.Solo;

            if (visitType == VisitType.Group && string.IsNullOrEmpty(dto.GroupId))
            {
                _logger.LogWarning("CheckAndStart failed – GroupId missing for group visit – userId: {UserId}", userId);
                return new() { IsSuccess = false, Message = "GroupId is required for group visits", ErrorType = "BadRequest" };
            }

            var visit = new Visit
            {
                UserId = userId,
                MuseumId = dto.MuseumId,
                VisitType = visitType,
                GroupId = visitType == VisitType.Group ? dto.GroupId : null,
                StartTime = DateTime.UtcNow,
                IsInsideMuseum = true
            };

            var created = await _visitRepo.StartVisitAsync(visit);

            _logger.LogInformation("Visit started – visitId: {VisitId}, userId: {UserId}, museumId: {MuseumId}",
                created.Id, userId, dto.MuseumId);

            return new()
            {
                IsSuccess = true,
                Message = "Welcome to the museum!",
                Data = new
                {
                    isInsideMuseum = true,
                    message = "Welcome to the museum!",
                    visit = MapToVisitDTO(created)
                }
            };
        }

        public async Task<ServiceResult<EndVisitDTO>> EndVisitAsync(string userId, string visitId)
        {
            _logger.LogInformation("Ending visit – visitId: {VisitId}, userId: {UserId}", visitId, userId);

            var visit = await _visitRepo.GetVisitByIdAsync(visitId);
            if (visit == null)
            {
                _logger.LogWarning("End visit failed – not found: {VisitId}", visitId);
                return new() { IsSuccess = false, Message = "Visit not found", ErrorType = "NotFound" };
            }

            if (visit.UserId != userId)
            {
                _logger.LogWarning("End visit forbidden – visitId: {VisitId}, requestingUserId: {UserId}", visitId, userId);
                return new() { IsSuccess = false, Message = "You are not authorized to end this visit", ErrorType = "Forbidden" };
            }

            if (visit.EndTime != null)
            {
                _logger.LogWarning("End visit failed – already ended: {VisitId}", visitId);
                return new() { IsSuccess = false, Message = "Visit has already ended", ErrorType = "BadRequest" };
            }

            var museum = await _museumRepo.GetByIdAsync(visit.MuseumId);
            var endedVisit = await _visitRepo.EndVisitAsync(visitId);

            _logger.LogInformation("Visit ended – visitId: {VisitId}, userId: {UserId}", visitId, userId);

            return new()
            {
                IsSuccess = true,
                Message = "Visit ended successfully",
                Data = new EndVisitDTO
                {
                    VisitId = endedVisit.Id,
                    StartTime = endedVisit.StartTime,
                    EndTime = endedVisit.EndTime,
                    MuseumName = museum?.Name,
                    MuseumLocation = museum?.Location,
                    MuseumGeneralInfo = museum?.GeneralInfo,
                    MuseumOpenHours = museum?.Open_Hours,
                    TicketEgyptAdultPrice = museum?.TicketEgyptAdultPrice ?? 0,
                    TicketEgyptStudentPrice = museum?.TicketEgyptStudentPrice ?? 0,
                    TicketForienerAdultPrice = museum?.TicketForienerAdultPrice ?? 0,
                    TicketForienerStudentPrice = museum?.TicketForienerStudentPrice ?? 0,
                }
            };
        }

        public async Task<ServiceResult<List<VisitDTO>>> GetMyVisitsAsync(string userId)
        {
            _logger.LogInformation("Fetching all visits for userId: {UserId}", userId);

            var visits = await _visitRepo.GetUserVisitsAsync(userId);

            _logger.LogInformation("Fetched {Count} visits for userId: {UserId}", visits.Count, userId);

            return new() { IsSuccess = true, Data = MapToVisitDTOList(visits) };
        }

        public async Task<ServiceResult<List<VisitDTO>>> GetMySoloVisitsAsync(string userId)
        {
            _logger.LogInformation("Fetching solo visits for userId: {UserId}", userId);

            var visits = await _visitRepo.GetUserVisitsByTypeAsync(userId, VisitType.Solo);

            _logger.LogInformation("Fetched {Count} solo visits for userId: {UserId}", visits.Count, userId);

            return new() { IsSuccess = true, Data = MapToVisitDTOList(visits) };
        }

        public async Task<ServiceResult<List<VisitDTO>>> GetMyGroupVisitsAsync(string userId)
        {
            _logger.LogInformation("Fetching group visits for userId: {UserId}", userId);

            var visits = await _visitRepo.GetUserVisitsByTypeAsync(userId, VisitType.Group);

            _logger.LogInformation("Fetched {Count} group visits for userId: {UserId}", visits.Count, userId);

            return new() { IsSuccess = true, Data = MapToVisitDTOList(visits) };
        }

        // ════════════════════════════════════════════════════════════════════
        // Private helpers
        // ════════════════════════════════════════════════════════════════════

        private List<VisitDTO> MapToVisitDTOList(List<Visit> visits)
        {
            return visits.Select(v => MapToVisitDTO(v)).ToList();
        }

        private VisitDTO MapToVisitDTO(Visit v) => new VisitDTO
        {
            Id = v.Id,
            UserId = v.UserId,
            MuseumId = v.MuseumId,
            VisitType = v.VisitType.ToString(),
            StartTime = v.StartTime,
            IsInsideMuseum = v.IsInsideMuseum
        };

        private bool IsUserInsideMuseum(double userLat, double userLon, double museumLat, double museumLon)
        {
            const double R = 6371000;
            var dLat = (museumLat - userLat) * Math.PI / 180;
            var dLon = (museumLon - userLon) * Math.PI / 180;
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                  + Math.Cos(userLat * Math.PI / 180) * Math.Cos(museumLat * Math.PI / 180)
                  * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c <= AllowedRadiusInMeters;
        }
    }
}
