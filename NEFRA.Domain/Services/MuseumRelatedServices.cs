using Microsoft.Extensions.Logging;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using NEEFRA_API.DTO;
using NEEFRA_API.Models;

namespace NEEFRA.Core.Services
{
    // ════════════════════════════════════════════════════════════════════
    // NearbyHotel
    // ════════════════════════════════════════════════════════════════════
    public class NearbyHotelService : INearbyHotelService
    {
        private readonly INearbyHotelRepository _repo;
        private readonly ILogger<NearbyHotelService> _logger;

        public NearbyHotelService(INearbyHotelRepository repo, ILogger<NearbyHotelService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<ServiceResult<List<NearbyHotelDTO>>> GetByMuseumIdAsync(string museumId)
        {
            _logger.LogInformation("Fetching nearby hotels for museum {MuseumId}", museumId);
            var hotels = await _repo.GetByMuseumIdAsync(museumId);
            return new() { IsSuccess = true, Data = hotels.Select(MapToDTO).ToList() };
        }

        public async Task<ServiceResult<NearbyHotelDTO>> AddAsync(CreateNearbyHotelDTO dto)
        {
            if (dto == null)
                return new() { IsSuccess = false, Message = "Data is not accurate", ErrorType = "BadRequest" };

            var hotel = new NearbyHotel
            {
                MuseumId = dto.MuseumId,
                Name = dto.Name,
                Address = dto.Address,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                DistanceInKm = dto.DistanceInKm,
                StarRating = dto.StarRating,
                PhoneNumber = dto.PhoneNumber,
                Website = dto.Website,
                PhotoUrl = dto.PhotoUrl
            };

            var created = await _repo.AddAsync(hotel);
            return new() { IsSuccess = true, Message = "Hotel added successfully", Data = MapToDTO(created) };
        }

        public async Task<ServiceResult<bool>> DeleteAsync(string id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted) return new() { IsSuccess = false, Message = "Hotel not found", ErrorType = "NotFound" };
            return new() { IsSuccess = true, Message = "Hotel deleted successfully", Data = true };
        }

        private NearbyHotelDTO MapToDTO(NearbyHotel h) => new()
        {
            Id = h.Id!, MuseumId = h.MuseumId, Name = h.Name, Address = h.Address,
            Latitude = h.Latitude, Longitude = h.Longitude, DistanceInKm = h.DistanceInKm,
            StarRating = h.StarRating, PhoneNumber = h.PhoneNumber, Website = h.Website, PhotoUrl = h.PhotoUrl
        };
    }

    // ════════════════════════════════════════════════════════════════════
    // NearbyRestaurant
    // ════════════════════════════════════════════════════════════════════
    public class NearbyRestaurantService : INearbyRestaurantService
    {
        private readonly INearbyRestaurantRepository _repo;
        private readonly ILogger<NearbyRestaurantService> _logger;

        public NearbyRestaurantService(INearbyRestaurantRepository repo, ILogger<NearbyRestaurantService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<ServiceResult<List<NearbyRestaurantDTO>>> GetByMuseumIdAsync(string museumId)
        {
            _logger.LogInformation("Fetching nearby restaurants for museum {MuseumId}", museumId);
            var restaurants = await _repo.GetByMuseumIdAsync(museumId);
            return new() { IsSuccess = true, Data = restaurants.Select(MapToDTO).ToList() };
        }

        public async Task<ServiceResult<NearbyRestaurantDTO>> AddAsync(CreateNearbyRestaurantDTO dto)
        {
            if (dto == null)
                return new() { IsSuccess = false, Message = "Data is not accurate", ErrorType = "BadRequest" };

            var restaurant = new NearbyRestaurant
            {
                MuseumId = dto.MuseumId,
                Name = dto.Name,
                Address = dto.Address,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                DistanceInKm = dto.DistanceInKm,
                CuisineType = dto.CuisineType,
                PriceRange = dto.PriceRange,
                PhoneNumber = dto.PhoneNumber,
                Website = dto.Website,
                PhotoUrl = dto.PhotoUrl
            };

            var created = await _repo.AddAsync(restaurant);
            return new() { IsSuccess = true, Message = "Restaurant added successfully", Data = MapToDTO(created) };
        }

        public async Task<ServiceResult<bool>> DeleteAsync(string id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted) return new() { IsSuccess = false, Message = "Restaurant not found", ErrorType = "NotFound" };
            return new() { IsSuccess = true, Message = "Restaurant deleted successfully", Data = true };
        }

        private NearbyRestaurantDTO MapToDTO(NearbyRestaurant r) => new()
        {
            Id = r.Id!, MuseumId = r.MuseumId, Name = r.Name, Address = r.Address,
            Latitude = r.Latitude, Longitude = r.Longitude, DistanceInKm = r.DistanceInKm,
            CuisineType = r.CuisineType, PriceRange = r.PriceRange,
            PhoneNumber = r.PhoneNumber, Website = r.Website, PhotoUrl = r.PhotoUrl
        };
    }

    // ════════════════════════════════════════════════════════════════════
    // GiftShop
    // ════════════════════════════════════════════════════════════════════
    public class GiftShopService : IGiftShopService
    {
        private readonly IGiftShopRepository _repo;
        private readonly ILogger<GiftShopService> _logger;

        public GiftShopService(IGiftShopRepository repo, ILogger<GiftShopService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<ServiceResult<List<GiftShopDTO>>> GetByMuseumIdAsync(string museumId)
        {
            _logger.LogInformation("Fetching gift shops for museum {MuseumId}", museumId);
            var shops = await _repo.GetByMuseumIdAsync(museumId);
            return new() { IsSuccess = true, Data = shops.Select(MapToDTO).ToList() };
        }

        public async Task<ServiceResult<GiftShopDTO>> AddAsync(CreateGiftShopDTO dto)
        {
            if (dto == null)
                return new() { IsSuccess = false, Message = "Data is not accurate", ErrorType = "BadRequest" };

            var shop = new GiftShop
            {
                MuseumId = dto.MuseumId,
                Name = dto.Name,
                Description = dto.Description,
                OpenHours = dto.OpenHours,
                Location = dto.Location,
                PhotoUrl = dto.PhotoUrl
            };

            var created = await _repo.AddAsync(shop);
            return new() { IsSuccess = true, Message = "Gift shop added successfully", Data = MapToDTO(created) };
        }

        public async Task<ServiceResult<bool>> DeleteAsync(string id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted) return new() { IsSuccess = false, Message = "Gift shop not found", ErrorType = "NotFound" };
            return new() { IsSuccess = true, Message = "Gift shop deleted successfully", Data = true };
        }

        private GiftShopDTO MapToDTO(GiftShop g) => new()
        {
            Id = g.Id!, MuseumId = g.MuseumId, Name = g.Name,
            Description = g.Description, OpenHours = g.OpenHours,
            Location = g.Location, PhotoUrl = g.PhotoUrl
        };
    }

    // ════════════════════════════════════════════════════════════════════
    // Cafe
    // ════════════════════════════════════════════════════════════════════
    public class CafeService : ICafeService
    {
        private readonly ICafeRepository _repo;
        private readonly ILogger<CafeService> _logger;

        public CafeService(ICafeRepository repo, ILogger<CafeService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<ServiceResult<List<CafeDTO>>> GetByMuseumIdAsync(string museumId)
        {
            _logger.LogInformation("Fetching cafes for museum {MuseumId}", museumId);
            var cafes = await _repo.GetByMuseumIdAsync(museumId);
            return new() { IsSuccess = true, Data = cafes.Select(MapToDTO).ToList() };
        }

        public async Task<ServiceResult<CafeDTO>> AddAsync(CreateCafeDTO dto)
        {
            if (dto == null)
                return new() { IsSuccess = false, Message = "Data is not accurate", ErrorType = "BadRequest" };

            var cafe = new Cafe
            {
                MuseumId = dto.MuseumId,
                Name = dto.Name,
                Description = dto.Description,
                OpenHours = dto.OpenHours,
                Location = dto.Location,
                PhotoUrl = dto.PhotoUrl,
                HasOutdoorSeating = dto.HasOutdoorSeating
            };

            var created = await _repo.AddAsync(cafe);
            return new() { IsSuccess = true, Message = "Cafe added successfully", Data = MapToDTO(created) };
        }

        public async Task<ServiceResult<bool>> DeleteAsync(string id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted) return new() { IsSuccess = false, Message = "Cafe not found", ErrorType = "NotFound" };
            return new() { IsSuccess = true, Message = "Cafe deleted successfully", Data = true };
        }

        private CafeDTO MapToDTO(Cafe c) => new()
        {
            Id = c.Id!, MuseumId = c.MuseumId, Name = c.Name,
            Description = c.Description, OpenHours = c.OpenHours,
            Location = c.Location, PhotoUrl = c.PhotoUrl, HasOutdoorSeating = c.HasOutdoorSeating
        };
    }
}
