using NEEFRA.Domain.IReposatory;
using NEEFRA_API.Models;

namespace NEEFRA_API.DataAccess.Reposatory.IReposatory
{
    public interface IArtPieceRepository : IReposatory<ArtPiece>
    {
        Task<List<ArtPiece>> GetAllAsync();
        Task<List<ArtPiece>> GetByMuseumIdAsync(string museumId);
        Task<ArtPiece> AddAsync(ArtPiece artPiece);
    }
}
