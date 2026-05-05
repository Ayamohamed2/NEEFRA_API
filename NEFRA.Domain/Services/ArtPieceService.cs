using Microsoft.Extensions.Logging;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using NEEFRA_API.DTO;
using NEEFRA_API.Models;

namespace NEEFRA.Core.Services
{
    public class ArtPieceService : IArtPieceService
    {
        private readonly IArtPieceRepository _artPieceRepo;
        private readonly ILogger<ArtPieceService> _logger;

        public ArtPieceService(IArtPieceRepository artPieceRepo, ILogger<ArtPieceService> logger)
        {
            _artPieceRepo = artPieceRepo;
            _logger = logger;
        }

        // ════════════════════════════════════════════════════════════════════
        // ArtPiece Operations
        // ════════════════════════════════════════════════════════════════════

        public async Task<ServiceResult<List<ArtPieceDTO>>> GetByMuseumAsync(string museumId)
        {
            _logger.LogInformation("Fetching art pieces for museumId: {MuseumId}", museumId);

            var pieces = await _artPieceRepo.GetByMuseumIdAsync(museumId);

            var data = pieces.Select(p => MapToDTO(p)).ToList();

            _logger.LogInformation("Fetched {Count} art pieces for museumId: {MuseumId}", data.Count, museumId);

            return new() { IsSuccess = true, Data = data };
        }

        public async Task<ServiceResult<ArtPieceDTO>> AddAsync(CreateArtPieceDTO dto)
        {
            _logger.LogInformation("Adding new art piece – name: {Name}, museumId: {MuseumId}", dto.Name, dto.MuseumId);

            var artPiece = new ArtPiece
            {
                Name = dto.Name,
                ImageUrl = dto.ImageUrl,
                MuseumId = dto.MuseumId,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Floor = dto.Floor
            };

            var created = await _artPieceRepo.AddAsync(artPiece);

            _logger.LogInformation("Art piece created – id: {Id}, museumId: {MuseumId}", created.Id, created.MuseumId);

            return new()
            {
                IsSuccess = true,
                Message = "Art piece created successfully",
                Data = MapToDTO(created)
            };
        }

        // ════════════════════════════════════════════════════════════════════
        // Private helpers
        // ════════════════════════════════════════════════════════════════════

        private ArtPieceDTO MapToDTO(ArtPiece p) => new ArtPieceDTO
        {
            Id = p.Id,
            Name = p.Name,
            ImageUrl = p.ImageUrl,
            MuseumId = p.MuseumId,
            Latitude = p.Latitude,
            Longitude = p.Longitude,
            Floor = p.Floor
        };
    }
}
