using MongoDB.Driver;
using NEEFRA_API.DataAccess.Data;
using NEEFRA_API.Models;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using Villa_API_Project.DataAccess.Reposatory;

namespace NEEFRA_API.DataAccess.Reposatory
{
    public class FavouriteRepository : Reposatory<Favourite>, IFavouriteRepository
    {
        private readonly IMongoCollection<Favourite> _favourites;

        public FavouriteRepository(MongoDbContext context):base(context)
        {
            _favourites = context.Favourites;
        }

        public async Task<Favourite> AddAsync(Favourite favourite)
        {
            await _favourites.InsertOneAsync(favourite);
            return favourite;
        }

        public async Task<List<Favourite>> GetUserFavouritesAsync(string userId)
        {
            return await _favourites
                .Find(f => f.UserId == userId)
                .ToListAsync();
        }
    }
}
