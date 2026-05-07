using MongoDB.Driver;
using NEEFRA_API.DataAccess.Data;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using NEEFRA_API.Models;

namespace NEEFRA_API.DataAccess.Reposatory
{
    public class MuseumFacilitiesRepository : IMuseumFacilitiesRepository
    {
        private readonly IMongoCollection<MuseumFacilities> _collection;

        public MuseumFacilitiesRepository(MongoDbContext context)
        {
            _collection = context.MuseumFacilities;
        }

        public async Task<MuseumFacilities?> GetByMuseumIdAsync(string museumId)
        {
            return await _collection.Find(f => f.MuseumId == museumId).FirstOrDefaultAsync();
        }

        public async Task<MuseumFacilities> AddAsync(MuseumFacilities facilities)
        {
            await _collection.InsertOneAsync(facilities);
            return facilities;
        }

        public async Task<MuseumFacilities?> UpdateAsync(string museumId, MuseumFacilities facilities)
        {
            var result = await _collection.FindOneAndReplaceAsync(
                f => f.MuseumId == museumId,
                facilities,
                new FindOneAndReplaceOptions<MuseumFacilities> { ReturnDocument = ReturnDocument.After }
            );
            return result;
        }
    }
}
