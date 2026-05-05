using NEEFRA.Core.Entities.Account;

namespace NEEFRA.Domain.IReposatory
{
    public interface IRevokedTokensReposatory : IReposatory<RevokedTokens>
    {
        Task AddRevokedTokenAsync(string token, DateTime expiryDate);
        Task<bool> IsTokenRevokedAsync(string token);
    }
}
