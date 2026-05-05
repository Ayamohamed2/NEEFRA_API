using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MongoDB.Driver;
using MongoDB.Driver;
using NEEFRA_API.DataAccess.Data;
using System.Security.Claims;
using System.Text;
using Org.BouncyCastle.Crypto.Generators;
using NEEFRA.Domain.IReposatory;
using NEEFRA.Core.Entities.Account;
using NEEFRA.Core.DTO.Account;

namespace Villa_API_Project.DataAccess.Reposatory
{
    public class AuthRepository:IAuthRepository
    {
        private readonly MongoDbContext _context;
        private readonly IJWT_TokenReposatory _token;
        private readonly IEmailSender _sendEmail;

        public AuthRepository(
            MongoDbContext context,
            IJWT_TokenReposatory token,
            IEmailSender sendEmail)
        {
            _context = context;
            _token = token;
            _sendEmail = sendEmail;
        }

        public async Task<TokenDTO> LoginAsync(LoginDTO model)
        {
            var user = await _context.ApplicationUsers
                .Find(u => u.Email == model.Email)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                
                return new TokenDTO { Message = "InvalidCredentials" };
            }

            if (!user.IsEmailConfirmed) 
                return new TokenDTO { Message = "EmailNotConfirmed" };

            if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.UtcNow)
                return new TokenDTO { Message = "LockedOut" };
            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                user.AccessFailedCount += 1;

                if (user.AccessFailedCount >= 5)
                {
                    user.LockoutEnd = DateTime.UtcNow.AddMinutes(1); 
                    user.AccessFailedCount = 0;
                    await _context.ApplicationUsers.ReplaceOneAsync(u => u.Id == user.Id, user);

                    return new TokenDTO { Message = "LockedOut" };
                }

                await _context.ApplicationUsers.ReplaceOneAsync(u => u.Id == user.Id, user);
                return new TokenDTO { Message = "InvalidCredentials" };
            }
            user.AccessFailedCount = 0;
            user.LockoutEnd = null;
            
            string jwtTokenId = $"JTI{Guid.NewGuid()}";
            var accessToken = await _token.GenerateToken(user, jwtTokenId);
            var refreshToken = await _token.CreateNewRefreshToken(user.Id, jwtTokenId);
            user.LastSeen = DateTime.UtcNow;
            await _context.ApplicationUsers.ReplaceOneAsync(u => u.Id == user.Id, user);

            return new TokenDTO
            {
                Message = "LoginSuccess",
                AccessToken = accessToken,
                RefreshToken=refreshToken
            };
        }

        public async Task<ApplicationUser> Register(RegisterDTO model)
        {
            var exists = await _context.ApplicationUsers
                .Find(u => u.Email == model.Email)
                .AnyAsync();

            if (exists)
             return   null;

            ApplicationUser user = new ApplicationUser
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "Customer",
                IsEmailConfirmed = false
            };

            await _context.ApplicationUsers.InsertOneAsync(user);
            Console.WriteLine(user);

            return user;
        }

       
    }
}
