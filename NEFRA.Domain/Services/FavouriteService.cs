using Microsoft.Extensions.Logging;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using NEEFRA_API.DTO;
using NEEFRA_API.Models;

namespace NEEFRA.Core.Services
{
    public class FavouriteService : IFavouriteService
    {
        private readonly IFavouriteRepository _favouriteRepo;
        private readonly ILogger<FavouriteService> _logger;

        public FavouriteService(IFavouriteRepository favouriteRepo, ILogger<FavouriteService> logger)
        {
            _favouriteRepo = favouriteRepo;
            _logger = logger;
        }

        // ════════════════════════════════════════════════════════════════════
        // Favourite Operations
        // ════════════════════════════════════════════════════════════════════

        public async Task<ServiceResult<FavouriteDTO>> AddFavouriteAsync(string userId, AddFavouriteDTO dto)
        {
            _logger.LogInformation("Adding favourite – userId: {UserId}, artPieceId: {ArtPieceId}", userId, dto.ArtPieceId);

            var favourite = new Favourite
            {
                UserId = userId,
                ArtPieceId = dto.ArtPieceId,
                VisitId = dto.VisitId,
                AddedAt = DateTime.UtcNow
            };

            var created = await _favouriteRepo.AddAsync(favourite);

            _logger.LogInformation("Favourite added – id: {Id}, userId: {UserId}", created.Id, userId);

            return new()
            {
                IsSuccess = true,
                Message = "Added to favourites successfully",
                Data = MapToDTO(created)
            };
        }

        public async Task<ServiceResult<List<FavouriteDTO>>> GetMyFavouritesAsync(string userId)
        {
            _logger.LogInformation("Fetching favourites for userId: {UserId}", userId);

            var favourites = await _favouriteRepo.GetUserFavouritesAsync(userId);

            var data = favourites.Select(f => MapToDTO(f)).ToList();

            _logger.LogInformation("Fetched {Count} favourites for userId: {UserId}", data.Count, userId);

            return new() { IsSuccess = true, Data = data };
        }

        // ════════════════════════════════════════════════════════════════════
        // Private helpers
        // ════════════════════════════════════════════════════════════════════

        private FavouriteDTO MapToDTO(Favourite f) => new FavouriteDTO
        {
            Id = f.Id,
            ArtPieceId = f.ArtPieceId,
            VisitId = f.VisitId,
            AddedAt = f.AddedAt
        };
    }
}
