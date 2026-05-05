using MongoDB.Driver;
using NEEFRA.Core.Entities.Account;
using NEEFRA.Domain.IReposatory;
using NEEFRA_API.DataAccess.Data;
using Villa_API_Project.DataAccess.Reposatory;

namespace NEEFRA_API.DataAccess.Reposatory
{
    public class PasswordResetTokensReposatory : Reposatory<PasswordResetToken>,IPasswordResetTokensReposatory
    {
        private readonly IMongoCollection<PasswordResetToken> collection;

        public PasswordResetTokensReposatory(MongoDbContext context):base(context)
        {
            collection = context.PasswordResetTokens;
        }

        public async Task AddAsync(PasswordResetToken token)
        {
            await collection.InsertOneAsync(token);
        }

        public async Task<PasswordResetToken?> GetValidTokenAsync(string token)
        {
            return await collection.Find(x =>
                x.Token == token &&
                x.ExpireAt > DateTime.UtcNow
            ).FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(string id)
        {
            await collection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
