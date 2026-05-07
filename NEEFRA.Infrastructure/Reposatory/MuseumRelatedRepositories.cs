using MongoDB.Driver;
using NEEFRA_API.DataAccess.Data;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using NEEFRA_API.Models;

namespace NEEFRA_API.DataAccess.Reposatory
{
    // ════════════════════════════════════════════════════════════════════
    // NearbyHotel
    // ════════════════════════════════════════════════════════════════════
    public class NearbyHotelRepository : INearbyHotelRepository
    {
        private readonly IMongoCollection<NearbyHotel> _collection;

        public NearbyHotelRepository(MongoDbContext context)
        {
            _collection = context.NearbyHotels;
        }

        public async Task<List<NearbyHotel>> GetByMuseumIdAsync(string museumId) =>
            await _collection.Find(h => h.MuseumId == museumId).ToListAsync();

        public async Task<NearbyHotel?> GetByIdAsync(string id) =>
            await _collection.Find(h => h.Id == id).FirstOrDefaultAsync();

        public async Task<NearbyHotel> AddAsync(NearbyHotel hotel)
        {
            await _collection.InsertOneAsync(hotel);
            return hotel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(h => h.Id == id);
            return result.DeletedCount > 0;
        }
    }

    // ════════════════════════════════════════════════════════════════════
    // NearbyRestaurant
    // ════════════════════════════════════════════════════════════════════
    public class NearbyRestaurantRepository : INearbyRestaurantRepository
    {
        private readonly IMongoCollection<NearbyRestaurant> _collection;

        public NearbyRestaurantRepository(MongoDbContext database)
        {
            _collection = database.NearbyRestaurants;
        }

        public async Task<List<NearbyRestaurant>> GetByMuseumIdAsync(string museumId) =>
            await _collection.Find(r => r.MuseumId == museumId).ToListAsync();

        public async Task<NearbyRestaurant?> GetByIdAsync(string id) =>
            await _collection.Find(r => r.Id == id).FirstOrDefaultAsync();

        public async Task<NearbyRestaurant> AddAsync(NearbyRestaurant restaurant)
        {
            await _collection.InsertOneAsync(restaurant);
            return restaurant;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(r => r.Id == id);
            return result.DeletedCount > 0;
        }
    }

    // ════════════════════════════════════════════════════════════════════
    // GiftShop
    // ════════════════════════════════════════════════════════════════════
    public class GiftShopRepository : IGiftShopRepository
    {
        private readonly IMongoCollection<GiftShop> _collection;

        public GiftShopRepository(MongoDbContext database)
        {
            _collection = database.GiftShops;
        }

        public async Task<List<GiftShop>> GetByMuseumIdAsync(string museumId) =>
            await _collection.Find(g => g.MuseumId == museumId).ToListAsync();

        public async Task<GiftShop?> GetByIdAsync(string id) =>
            await _collection.Find(g => g.Id == id).FirstOrDefaultAsync();

        public async Task<GiftShop> AddAsync(GiftShop giftShop)
        {
            await _collection.InsertOneAsync(giftShop);
            return giftShop;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(g => g.Id == id);
            return result.DeletedCount > 0;
        }
    }

    // ════════════════════════════════════════════════════════════════════
    // Cafe
    // ════════════════════════════════════════════════════════════════════
    public class CafeRepository : ICafeRepository
    {
        private readonly IMongoCollection<Cafe> _collection;

        public CafeRepository(MongoDbContext database)
        {
            _collection = database.Cafes;
        }

        public async Task<List<Cafe>> GetByMuseumIdAsync(string museumId) =>
            await _collection.Find(c => c.MuseumId == museumId).ToListAsync();

        public async Task<Cafe?> GetByIdAsync(string id) =>
            await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();

        public async Task<Cafe> AddAsync(Cafe cafe)
        {
            await _collection.InsertOneAsync(cafe);
            return cafe;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(c => c.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
