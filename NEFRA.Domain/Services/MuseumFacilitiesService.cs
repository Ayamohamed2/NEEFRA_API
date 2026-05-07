using Microsoft.Extensions.Logging;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using NEEFRA_API.DTO;
using NEEFRA_API.Models;

namespace NEEFRA.Core.Services
{
    public class MuseumFacilitiesService : IMuseumFacilitiesService
    {
        private readonly IMuseumFacilitiesRepository _repo;
        private readonly ILogger<MuseumFacilitiesService> _logger;

        public MuseumFacilitiesService(IMuseumFacilitiesRepository repo, ILogger<MuseumFacilitiesService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<ServiceResult<MuseumFacilitiesDTO>> GetByMuseumIdAsync(string museumId)
        {
            _logger.LogInformation("Fetching facilities for museum {MuseumId}", museumId);
            var facilities = await _repo.GetByMuseumIdAsync(museumId);

            if (facilities == null)
                return new() { IsSuccess = false, Message = "Facilities not found", ErrorType = "NotFound" };

            return new() { IsSuccess = true, Data = MapToDTO(facilities) };
        }

        public async Task<ServiceResult<MuseumFacilitiesDTO>> AddAsync(CreateMuseumFacilitiesDTO dto)
        {
            _logger.LogInformation("Adding facilities for museum {MuseumId}", dto.MuseumId);

            if (dto == null)
                return new() { IsSuccess = false, Message = "Data is not accurate", ErrorType = "BadRequest" };

            var existing = await _repo.GetByMuseumIdAsync(dto.MuseumId);
            if (existing != null)
                return new() { IsSuccess = false, Message = "Facilities already exist for this museum. Use PUT to update.", ErrorType = "Conflict" };

            var facilities = new MuseumFacilities
            {
                MuseumId = dto.MuseumId,
                HasWifi = dto.HasWifi,
                IsWheelchairAccessible = dto.IsWheelchairAccessible,
                HasAudioGuide = dto.HasAudioGuide,
                HasLockers = dto.HasLockers,
                WifiPassword = dto.WifiPassword,
                AudioGuideLanguages = dto.AudioGuideLanguages,
                Notes = dto.Notes
            };

            var created = await _repo.AddAsync(facilities);
            _logger.LogInformation("MuseumFacilities created – id: {Id}", created.Id);
            return new() { IsSuccess = true, Message = "Facilities added successfully", Data = MapToDTO(created) };
        }

        public async Task<ServiceResult<MuseumFacilitiesDTO>> UpdateAsync(string museumId, UpdateMuseumFacilitiesDTO dto)
        {
            _logger.LogInformation("Updating facilities for museum {MuseumId}", museumId);

            var existing = await _repo.GetByMuseumIdAsync(museumId);
            if (existing == null)
                return new() { IsSuccess = false, Message = "Facilities not found", ErrorType = "NotFound" };

            existing.HasWifi = dto.HasWifi;
            existing.IsWheelchairAccessible = dto.IsWheelchairAccessible;
            existing.HasAudioGuide = dto.HasAudioGuide;
            existing.HasLockers = dto.HasLockers;
            existing.WifiPassword = dto.WifiPassword;
            existing.AudioGuideLanguages = dto.AudioGuideLanguages;
            existing.Notes = dto.Notes;

            var updated = await _repo.UpdateAsync(museumId, existing);
            return new() { IsSuccess = true, Message = "Facilities updated successfully", Data = MapToDTO(updated!) };
        }

        private MuseumFacilitiesDTO MapToDTO(MuseumFacilities f) => new()
        {
            Id = f.Id!,
            MuseumId = f.MuseumId,
            HasWifi = f.HasWifi,
            IsWheelchairAccessible = f.IsWheelchairAccessible,
            HasAudioGuide = f.HasAudioGuide,
            HasLockers = f.HasLockers,
            WifiPassword = f.WifiPassword,
            AudioGuideLanguages = f.AudioGuideLanguages,
            Notes = f.Notes
        };
    }
}
