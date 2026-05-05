using NEEFRA.Core.Entities.Account;

namespace NEEFRA.Domain.IReposatory
{
    public interface IPasswordResetTokensReposatory :IReposatory<PasswordResetToken>
    {
        Task AddAsync(PasswordResetToken token);
        Task<PasswordResetToken?> GetValidTokenAsync(string token);
        Task DeleteAsync(string id);

    }
}
