using MongoDB.Driver;
using NEEFRA_API.DataAccess.Data;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using NEEFRA_API.Models;
using Villa_API_Project.DataAccess.Reposatory;

namespace NEEFRA_API.DataAccess.Reposatory
{
    public class VisitRepository : Reposatory<Visit>,IVisitRepository
    {
        private readonly IMongoCollection<Visit> _visits;

        public VisitRepository(MongoDbContext context):base(context)
        {
            _visits = context.Visits;
        }
        public async Task<Visit> StartVisitAsync(Visit visit)
        {
            await _visits.InsertOneAsync(visit);
            return visit;
        }
        public async Task<Visit?> GetVisitByIdAsync(string visitId)
        {
            return await _visits
                .Find(v => v.Id == visitId)
                .FirstOrDefaultAsync();
        }
        public async Task<Visit> EndVisitAsync(string visitId)
        {
            var update = Builders<Visit>.Update
                .Set(v => v.EndTime, DateTime.UtcNow)
                .Set(v => v.IsInsideMuseum, false);

            var options = new FindOneAndUpdateOptions<Visit>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await _visits.FindOneAndUpdateAsync(
                v => v.Id == visitId,
                update,
                options
            );
        }

        public async Task<List<Visit>> GetUserVisitsAsync(string userId)
        {
            return await _visits
                .Find(v => v.UserId == userId)
                .ToListAsync();
        }
        // ✅ بيتشك لو في visit مفتوحة على نفس المتحف
        public async Task<Visit?> GetActiveVisitAsync(string userId, string museumId)
        {
            return await _visits
                .Find(v => v.UserId == userId
                       && v.MuseumId == museumId
                       && v.EndTime == null)
                .FirstOrDefaultAsync();
        }


        // لل solo visits , group visits
        public async Task<List<Visit>> GetUserVisitsByTypeAsync(string userId, VisitType visitType)
        {
            return await _visits
                .Find(v => v.UserId == userId && v.VisitType == visitType)
                .ToListAsync();
        }
    }
}
