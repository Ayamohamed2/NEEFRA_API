using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using NEEFRA.Core.DTO.Account;
using NEEFRA.Core.Entities.Account;
using NEEFRA.Domain.IReposatory;
using NEEFRA_API.DataAccess.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Villa_API_Project.Models;

namespace Villa_API_Project.DataAccess.Reposatory
{
    public class JWT_TokenReposatory:IJWT_TokenReposatory
    {
        private readonly IConfiguration _config;

        IUnitOfWork unit;
        MongoDbContext context;

        public JWT_TokenReposatory(IConfiguration config, IUnitOfWork unit, MongoDbContext context)
        {
            _config = config;
            this.unit = unit;
            this.context = context;
        }

        public Task<string> GenerateToken(ApplicationUser user, string jwtTokenId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, jwtTokenId)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(2),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Task.FromResult(tokenString);
        }


        public async Task<TokenDTO> RefreshAccessToken(TokenDTO tokenDTO)
        {
            var existingRefreshToken =await unit.RefreshToken.GetByFilterAsync(u => u.Refresh_Token == tokenDTO.RefreshToken);
            if (existingRefreshToken == null)
            {
                return new TokenDTO();
            }

            var isTokenValid = GetAccessTokenData(tokenDTO.AccessToken, existingRefreshToken.UserId, existingRefreshToken.JwtTokenId);
            if (!isTokenValid)
            {
                await MarkTokenAsInvalid(existingRefreshToken);
                return new TokenDTO();
            }

            if (!existingRefreshToken.IsValid)
            {
                await MarkAllTokenInChainAsInvalid(existingRefreshToken.UserId, existingRefreshToken.JwtTokenId);
                return new TokenDTO();
            }
            if (existingRefreshToken.ExpiresAt < DateTime.UtcNow)
            {
              await   MarkTokenAsInvalid(existingRefreshToken);
                return new TokenDTO();
            }

            var newRefreshToken =await CreateNewRefreshToken(existingRefreshToken.UserId, existingRefreshToken.JwtTokenId);


            await MarkTokenAsInvalid(existingRefreshToken);

            // generate new access token
            var applicationUser =await unit.Users.GetByFilterAsync(u => u.Id == existingRefreshToken.UserId);
            if (applicationUser == null)
                return new TokenDTO();

            var newAccessToken = await GenerateToken(applicationUser, existingRefreshToken.JwtTokenId);

            return new TokenDTO()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };

        }

        public async Task RevokeAllTokens(TokenDTO tokenDTO)
        {
            var existingRefreshToken =await unit.RefreshToken.GetByFilterAsync(r => r.Refresh_Token == tokenDTO.RefreshToken);

            if (existingRefreshToken == null)
                return;



            var isTokenValid = GetAccessTokenData(tokenDTO.AccessToken, existingRefreshToken.UserId, existingRefreshToken.JwtTokenId);
            if (!isTokenValid)
            {

                return;
            }

            await MarkAllTokenInChainAsInvalid(existingRefreshToken.UserId, existingRefreshToken.JwtTokenId);
            //logout jwt token
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(tokenDTO.AccessToken);
            var expiryDate = jwtToken.ValidTo;

            await unit.RevokedTokens.AddRevokedTokenAsync(tokenDTO.AccessToken, expiryDate);

        }

        public async Task<string> CreateNewRefreshToken(string userId, string tokenId)
        {
            RefreshToken refreshToken = new()
            {
                IsValid = true,
                UserId = userId,
                JwtTokenId = tokenId,
                ExpiresAt = DateTime.UtcNow.AddDays(2),
                Refresh_Token = Guid.NewGuid() + "-" + Guid.NewGuid(),
            };

           await unit.RefreshToken.CreateAsync(refreshToken);
            return refreshToken.Refresh_Token;
        }

        private bool GetAccessTokenData(string accessToken, string expectedUserId, string expectedTokenId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwt = tokenHandler.ReadJwtToken(accessToken);
                var jwtTokenId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Jti).Value;
                var userId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value;
                return userId == expectedUserId && jwtTokenId == expectedTokenId;

            }
            catch
            {
                return false;
            }
        }


        private async Task MarkAllTokenInChainAsInvalid(string userId, string tokenId)
        {
       
                var filter = Builders<RefreshToken>.Filter.And(
                    Builders<RefreshToken>.Filter.Eq(x => x.UserId, userId),
                    Builders<RefreshToken>.Filter.Eq(x => x.JwtTokenId, tokenId)
                );

                var update = Builders<RefreshToken>.Update
                    .Set(x => x.IsValid, false);

                await context.RefreshTokens.UpdateManyAsync(filter, update);
     

        }


        private async Task   MarkTokenAsInvalid(RefreshToken refreshToken)
        {
            refreshToken.IsValid = false;

            await unit.RefreshToken.UpdateAsync(r => r.Id == refreshToken.Id, refreshToken);
       
        }




    }
}
