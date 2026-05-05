using MongoDB.Driver;
using NEEFRA_API.DataAccess.Data;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using NEEFRA_API.Models;
using Villa_API_Project.DataAccess.Reposatory;
namespace NEEFRA_API.DataAccess.Reposatory
{
    public class ArtPieceRepository : Reposatory<ArtPiece>, IArtPieceRepository
    {
        private readonly IMongoCollection<ArtPiece> _artPieces;

        public ArtPieceRepository(MongoDbContext context):base(context)
        {
            _artPieces = context.ArtPieces;
        }

        public async Task<List<ArtPiece>> GetAllAsync()
        {
            return await _artPieces.Find(_ => true).ToListAsync();
        }

        public async Task<List<ArtPiece>> GetByMuseumIdAsync(string museumId)
        {
            return await _artPieces
                .Find(a => a.MuseumId == museumId)
                .ToListAsync();
        }

        public async Task<ArtPiece> AddAsync(ArtPiece artPiece)
        {
            await _artPieces.InsertOneAsync(artPiece);
            return artPiece;
        }
    }
}
