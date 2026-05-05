using NEEFRA.Core.DTO.Account;
using NEEFRA.Core.Entities.Account;

namespace NEEFRA.Domain.IReposatory
{
    public interface IAuthRepository
    {
        Task<ApplicationUser> Register(RegisterDTO model);
        Task<TokenDTO> LoginAsync(LoginDTO model);
    }
}
