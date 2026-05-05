using MongoDB.Driver;
using NEEFRA.Core.Entities;
using NEEFRA_API.DataAccess.Data;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using Villa_API_Project.DataAccess.Reposatory;

namespace NEEFRA_API.DataAccess.Reposatory
{
    public class GovernorateRepository : Reposatory<Governorate>, IGovernorateRepository
    {
        private readonly IMongoCollection<Governorate> _governorates;

        public GovernorateRepository(MongoDbContext context):base(context)
        {
            _governorates = context.Governorates;
        }

        public async Task<List<Governorate>> GetAllAsync()
        {
            return await _governorates.Find(_ => true).ToListAsync();
        }

        public async Task<Governorate> AddAsync(Governorate governorate)
        {
            await _governorates.InsertOneAsync(governorate);
            return governorate;
        }
    }
}
