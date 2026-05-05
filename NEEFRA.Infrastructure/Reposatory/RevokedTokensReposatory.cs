using MongoDB.Driver;
using NEEFRA.Core.Entities.Account;
using NEEFRA.Domain.IReposatory;
using NEEFRA_API.DataAccess.Data;

namespace Villa_API_Project.DataAccess.Reposatory
{
    public class RevokedTokensReposatory:Reposatory<RevokedTokens>,IRevokedTokensReposatory
    {

        MongoDbContext Context;
        public RevokedTokensReposatory(MongoDbContext context) : base(context)
        {
            this.Context = context;

        }
        public async Task AddRevokedTokenAsync(string token, DateTime expiryDate)
        {
            var revoked = new RevokedTokens
            {
                Token = token,
                ExpiredAt = expiryDate
            };

            await Context.RevokedTokens.InsertOneAsync(revoked);
        }

        public async Task<bool> IsTokenRevokedAsync(string token)
        {
            var filter = Builders<RevokedTokens>.Filter.Eq(x => x.Token, token);
            return await Context.RevokedTokens.Find(filter).AnyAsync();
        }
    }
}
