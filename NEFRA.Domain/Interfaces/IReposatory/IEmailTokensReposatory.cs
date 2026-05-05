using NEEFRA.Core.Entities.Account;

namespace NEEFRA.Domain.IReposatory
{
    public interface IEmailTokensReposatory:IReposatory<EmailConfirmationToken>
    {
        Task AddAsync(EmailConfirmationToken token);
        Task<EmailConfirmationToken?> GetValidTokenAsync(string userId, string token);
        Task DeleteAsync(string id);
    }
}
