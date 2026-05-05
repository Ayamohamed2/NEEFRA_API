using NEEFRA.Domain.IReposatory;
using NEEFRA_API.Models;

namespace NEEFRA_API.DataAccess.Reposatory.IReposatory
{
    public interface IFavouriteRepository : IReposatory<Favourite>
    {
        Task<Favourite> AddAsync(Favourite favourite);
        Task<List<Favourite>> GetUserFavouritesAsync(string userId);
    }
}
