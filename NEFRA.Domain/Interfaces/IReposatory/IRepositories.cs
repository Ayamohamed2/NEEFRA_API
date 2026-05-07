using NEEFRA_API.Models;

namespace NEEFRA_API.DataAccess.Reposatory.IReposatory
{
    public interface IGovernoratePhotoRepository
    {
        Task<List<GovernoratePhoto>> GetByGovernorateIdAsync(string governorateId);
        Task<GovernoratePhoto?> GetByIdAsync(string id);
        Task<GovernoratePhoto> AddAsync(GovernoratePhoto photo);
        Task<bool> DeleteAsync(string id);
    }

    public interface IMuseumFacilitiesRepository
    {
        Task<MuseumFacilities?> GetByMuseumIdAsync(string museumId);
        Task<MuseumFacilities> AddAsync(MuseumFacilities facilities);
        Task<MuseumFacilities?> UpdateAsync(string museumId, MuseumFacilities facilities);
    }

    public interface INearbyHotelRepository
    {
        Task<List<NearbyHotel>> GetByMuseumIdAsync(string museumId);
        Task<NearbyHotel?> GetByIdAsync(string id);
        Task<NearbyHotel> AddAsync(NearbyHotel hotel);
        Task<bool> DeleteAsync(string id);
    }

    public interface INearbyRestaurantRepository
    {
        Task<List<NearbyRestaurant>> GetByMuseumIdAsync(string museumId);
        Task<NearbyRestaurant?> GetByIdAsync(string id);
        Task<NearbyRestaurant> AddAsync(NearbyRestaurant restaurant);
        Task<bool> DeleteAsync(string id);
    }

    public interface IGiftShopRepository
    {
        Task<List<GiftShop>> GetByMuseumIdAsync(string museumId);
        Task<GiftShop?> GetByIdAsync(string id);
        Task<GiftShop> AddAsync(GiftShop giftShop);
        Task<bool> DeleteAsync(string id);
    }

    public interface ICafeRepository
    {
        Task<List<Cafe>> GetByMuseumIdAsync(string museumId);
        Task<Cafe?> GetByIdAsync(string id);
        Task<Cafe> AddAsync(Cafe cafe);
        Task<bool> DeleteAsync(string id);
    }
}
