using MongoDB.Driver;
using NEEFRA.Core.Entities.Account;
using NEEFRA.Domain.IReposatory;
using NEEFRA_API.DataAccess.Data;
using Villa_API_Project.DataAccess.Reposatory;

namespace NEEFRA_API.DataAccess.Reposatory
{
    public class EmailTokensReposatory :Reposatory<EmailConfirmationToken>, IEmailTokensReposatory
    {
        private readonly IMongoCollection<EmailConfirmationToken> collection;

        public EmailTokensReposatory(MongoDbContext context):base(context)
        {
            collection = context.EmailConfirmationTokens;
        }

        public async Task AddAsync(EmailConfirmationToken token)
        {
            await collection.InsertOneAsync(token);
        }

        public async Task<EmailConfirmationToken?> GetValidTokenAsync(string userId, string token)
        {
            return await collection.Find(x =>
                x.UserId == userId &&
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
