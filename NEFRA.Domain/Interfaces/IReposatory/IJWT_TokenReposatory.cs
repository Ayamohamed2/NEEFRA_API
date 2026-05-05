using NEEFRA.Core.DTO.Account;
using NEEFRA.Core.Entities.Account;

namespace NEEFRA.Domain.IReposatory
{
    public interface IJWT_TokenReposatory
    {
        Task<string> GenerateToken(ApplicationUser user, string jwtTokenId);
        Task<TokenDTO> RefreshAccessToken(TokenDTO tokenDTO);
        Task RevokeAllTokens(TokenDTO tokenDTO);
        Task<string> CreateNewRefreshToken(string userId, string tokenId);
    }
}
