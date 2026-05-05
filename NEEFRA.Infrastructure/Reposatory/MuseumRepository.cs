using MongoDB.Driver;
using NEEFRA.Core.Entities;
using NEEFRA_API.DataAccess.Data;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using Villa_API_Project.DataAccess.Reposatory;

namespace NEEFRA_API.DataAccess.Reposatory
{
    public class MuseumRepository : Reposatory<Museum>, IMuseumRepository
    {
        private readonly IMongoCollection<Museum> _museums;
        private readonly IMongoCollection<Governorate> _governorates;

        public MuseumRepository(MongoDbContext context):base(context)
        {
            _museums = context.Museums;
            _governorates = context.Governorates;
        }

        public async Task<List<Museum>> GetAllMuseumsAsync()
        {
            var museums = await _museums.Find(_ => true).ToListAsync();
            foreach (var museum in museums)
            {
                museum.Governorate = await _governorates
                    .Find(g => g.Id == museum.GovernorateId)
                    .FirstOrDefaultAsync();
            }
            return museums;
        }

       
        public async Task<List<Museum>> GetMuseumsByGovernorateAsync(string governorateId)
        {
            var museums = await _museums
                .Find(m => m.GovernorateId == governorateId)
                .ToListAsync();

            foreach (var museum in museums)
            {
                museum.Governorate = await _governorates
                    .Find(g => g.Id == museum.GovernorateId)
                    .FirstOrDefaultAsync();
            }
            return museums;
        }
        public async Task<Museum?> GetByIdAsync(string museumId)
        {
            return await _museums
                .Find(m => m.Id == museumId)
                .FirstOrDefaultAsync();
        }

        public async Task<Museum> AddMuseumAsync(Museum museum)
        {
            await _museums.InsertOneAsync(museum);
            return museum;
        }
    }
}
