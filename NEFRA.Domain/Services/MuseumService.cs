using Microsoft.Extensions.Logging;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Entities;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using NEEFRA_API.DTO;
using NEEFRA_API.Models;

namespace NEEFRA.Core.Services
{
    public class MuseumService : IMuseumService
    {
        private readonly IMuseumRepository _museumRepo;
        private readonly ILogger<MuseumService> _logger;

        public MuseumService(IMuseumRepository museumRepo, ILogger<MuseumService> logger)
        {
            _museumRepo = museumRepo;
            _logger = logger;
        }

        // ════════════════════════════════════════════════════════════════════
        // Museum Operations
        // ════════════════════════════════════════════════════════════════════

        public async Task<ServiceResult<List<MuseumDTO>>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all museums");

            var museums = await _museumRepo.GetAllMuseumsAsync();

            var data = museums.Select(m => MapToDTO(m)).ToList();

            _logger.LogInformation("Fetched {Count} museums", data.Count);

            return new() { IsSuccess = true, Data = data };
        }

        public async Task<ServiceResult<List<MuseumDTO>>> GetByGovernorateAsync(string governorateId)
        {
            _logger.LogInformation("Fetching museums by governorateId: {GovernorateId}", governorateId);

            var museums = await _museumRepo.GetMuseumsByGovernorateAsync(governorateId);

            if (!museums.Any())
            {
                _logger.LogWarning("No museums found for governorateId: {GovernorateId}", governorateId);
                return new() { IsSuccess = false, Message = "No museums found in this governorate", ErrorType = "NotFound" };
            }

            var data = museums.Select(m => MapToDTO(m)).ToList();

            _logger.LogInformation("Fetched {Count} museums for governorateId: {GovernorateId}", data.Count, governorateId);

            return new() { IsSuccess = true, Data = data };
        }

        public async Task<ServiceResult<MuseumDTO>> AddAsync(CreateMuseumDTO dto)
        {
            _logger.LogInformation("Adding new museum – name: {Name}", dto.Name);

            if (dto == null)
            {
                _logger.LogWarning("Add museum failed – dto is null");
                return new() { IsSuccess = false, Message = "Data is not accurate", ErrorType = "BadRequest" };
            }

            var museum = new Museum
            {
                Name = dto.Name,
                Location = dto.Location,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                GeneralInfo = dto.GeneralInfo,
                Open_Hours = dto.Open_Hours,
                TicketEgyptAdultPrice = dto.TicketEgyptAdultPrice,
                TicketEgyptStudentPrice = dto.TicketEgyptStudentPrice,
                TicketForienerAdultPrice = dto.TicketForienerAdultPrice,
                TicketForienerStudentPrice = dto.TicketForienerStudentPrice,
                GovernorateId = dto.GovernorateId
            };

            var created = await _museumRepo.AddMuseumAsync(museum);

            _logger.LogInformation("Museum created – id: {Id}, name: {Name}", created.Id, created.Name);

            return new()
            {
                IsSuccess = true,
                Message = "Museum added successfully",
                Data = MapToDTO(created)
            };
        }

        // ════════════════════════════════════════════════════════════════════
        // Private helpers
        // ════════════════════════════════════════════════════════════════════

        private MuseumDTO MapToDTO(Museum m) => new MuseumDTO
        {
            Id = m.Id,
            Name = m.Name,
            Location = m.Location,
            Latitude = m.Latitude,
            Longitude = m.Longitude,
            GeneralInfo = m.GeneralInfo,
            Open_Hours = m.Open_Hours,
            TicketEgyptAdultPrice = m.TicketEgyptAdultPrice,
            TicketEgyptStudentPrice = m.TicketEgyptStudentPrice,
            TicketForienerAdultPrice = m.TicketForienerAdultPrice,
            TicketForienerStudentPrice = m.TicketForienerStudentPrice,
            GovernorateName = m.Governorate?.Name
        };
    }
}
