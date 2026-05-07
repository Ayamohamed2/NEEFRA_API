using MongoDB.Driver;
using NEEFRA_API.DataAccess.Data;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using NEEFRA_API.Models;

namespace NEEFRA_API.DataAccess.Reposatory
{
    public class GovernoratePhotoRepository : IGovernoratePhotoRepository
    {
        private readonly IMongoCollection<GovernoratePhoto> _collection;

        public GovernoratePhotoRepository(MongoDbContext database)
        {
            _collection = database.GovernoratePhotos;
        }

        public async Task<List<GovernoratePhoto>> GetByGovernorateIdAsync(string governorateId)
        {
            return await _collection
                .Find(p => p.GovernorateId == governorateId)
                .ToListAsync();
        }

        public async Task<GovernoratePhoto?> GetByIdAsync(string id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<GovernoratePhoto> AddAsync(GovernoratePhoto photo)
        {
            await _collection.InsertOneAsync(photo);
            return photo;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(p => p.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
