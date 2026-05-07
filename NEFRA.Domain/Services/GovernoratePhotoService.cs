using Microsoft.Extensions.Logging;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using NEEFRA_API.DTO;
using NEEFRA_API.Models;

namespace NEEFRA.Core.Services
{
    public class GovernoratePhotoService : IGovernoratePhotoService
    {
        private readonly IGovernoratePhotoRepository _repo;
        private readonly ILogger<GovernoratePhotoService> _logger;

        public GovernoratePhotoService(IGovernoratePhotoRepository repo, ILogger<GovernoratePhotoService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<ServiceResult<List<GovernoratePhotoDTO>>> GetByGovernorateIdAsync(string governorateId)
        {
            _logger.LogInformation("Fetching photos for governorate {GovernorateId}", governorateId);
            var photos = await _repo.GetByGovernorateIdAsync(governorateId);
            return new() { IsSuccess = true, Data = photos.Select(MapToDTO).ToList() };
        }

        public async Task<ServiceResult<GovernoratePhotoDTO>> AddAsync(CreateGovernoratePhotoDTO dto)
        {
            _logger.LogInformation("Adding photo for governorate {GovernorateId}", dto.GovernorateId);

            if (dto == null)
                return new() { IsSuccess = false, Message = "Data is not accurate", ErrorType = "BadRequest" };

            var photo = new GovernoratePhoto
            {
                GovernorateId = dto.GovernorateId,
                PhotoUrl = dto.PhotoUrl,
                Caption = dto.Caption,
                IsPrimary = dto.IsPrimary,
                UploadedAt = DateTime.UtcNow
            };

            var created = await _repo.AddAsync(photo);
            _logger.LogInformation("GovernoratePhoto created – id: {Id}", created.Id);
            return new() { IsSuccess = true, Message = "Photo added successfully", Data = MapToDTO(created) };
        }

        public async Task<ServiceResult<bool>> DeleteAsync(string id)
        {
            _logger.LogInformation("Deleting governorate photo {Id}", id);
            var deleted = await _repo.DeleteAsync(id);

            if (!deleted)
                return new() { IsSuccess = false, Message = "Photo not found", ErrorType = "NotFound" };

            return new() { IsSuccess = true, Message = "Photo deleted successfully", Data = true };
        }

        private GovernoratePhotoDTO MapToDTO(GovernoratePhoto p) => new()
        {
            Id = p.Id!,
            GovernorateId = p.GovernorateId,
            PhotoUrl = p.PhotoUrl,
            Caption = p.Caption,
            IsPrimary = p.IsPrimary,
            UploadedAt = p.UploadedAt
        };
    }
}
