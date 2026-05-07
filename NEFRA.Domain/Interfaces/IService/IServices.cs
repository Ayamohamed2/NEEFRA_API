using NEEFRA.Core.DTO.Service;
using NEEFRA_API.DTO;

namespace NEEFRA.Core.Interfaces.IService
{
    public interface IGovernoratePhotoService
    {
        Task<ServiceResult<List<GovernoratePhotoDTO>>> GetByGovernorateIdAsync(string governorateId);
        Task<ServiceResult<GovernoratePhotoDTO>> AddAsync(CreateGovernoratePhotoDTO dto);
        Task<ServiceResult<bool>> DeleteAsync(string id);
    }

    public interface IMuseumFacilitiesService
    {
        Task<ServiceResult<MuseumFacilitiesDTO>> GetByMuseumIdAsync(string museumId);
        Task<ServiceResult<MuseumFacilitiesDTO>> AddAsync(CreateMuseumFacilitiesDTO dto);
        Task<ServiceResult<MuseumFacilitiesDTO>> UpdateAsync(string museumId, UpdateMuseumFacilitiesDTO dto);
    }

    public interface INearbyHotelService
    {
        Task<ServiceResult<List<NearbyHotelDTO>>> GetByMuseumIdAsync(string museumId);
        Task<ServiceResult<NearbyHotelDTO>> AddAsync(CreateNearbyHotelDTO dto);
        Task<ServiceResult<bool>> DeleteAsync(string id);
    }

    public interface INearbyRestaurantService
    {
        Task<ServiceResult<List<NearbyRestaurantDTO>>> GetByMuseumIdAsync(string museumId);
        Task<ServiceResult<NearbyRestaurantDTO>> AddAsync(CreateNearbyRestaurantDTO dto);
        Task<ServiceResult<bool>> DeleteAsync(string id);
    }

    public interface IGiftShopService
    {
        Task<ServiceResult<List<GiftShopDTO>>> GetByMuseumIdAsync(string museumId);
        Task<ServiceResult<GiftShopDTO>> AddAsync(CreateGiftShopDTO dto);
        Task<ServiceResult<bool>> DeleteAsync(string id);
    }

    public interface ICafeService
    {
        Task<ServiceResult<List<CafeDTO>>> GetByMuseumIdAsync(string museumId);
        Task<ServiceResult<CafeDTO>> AddAsync(CreateCafeDTO dto);
        Task<ServiceResult<bool>> DeleteAsync(string id);
    }
}
