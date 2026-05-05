using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NEEFRA.Core.Entities;
using NEEFRA.Core.Entities.Account;
using NEEFRA.Core.Entities.Group;
using NEEFRA.Core.Entities.Inerests;
using NEEFRA.Core.Entities.Piece;
using NEEFRA.Core.Entities.Route;
using NEEFRA_API.Models;
using NEEFRA_API.Settings;
using System.Text.RegularExpressions;
using Villa_API_Project.Models;
using Group = NEEFRA.Core.Entities.Group.Group;
namespace NEEFRA_API.DataAccess.Data
{
    public class MongoDbContext
    {
        public IMongoDatabase Database { get; }

        public MongoDbContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            Database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<ApplicationUser> ApplicationUsers =>
            Database.GetCollection<ApplicationUser>("ApplicationUsers");
        public IMongoCollection<Group> Groups =>
         Database.GetCollection<Group>("Groups");

        public IMongoCollection<GroupMember> GroupMembers =>
            Database.GetCollection<GroupMember>("GroupMembers");

        public IMongoCollection<GroupMessage> GroupMessages =>
            Database.GetCollection<GroupMessage>("GroupMessages");

        public IMongoCollection<GroupMessageRead> GroupMessageReads =>
            Database.GetCollection<GroupMessageRead>("GroupMessageReads");

        public IMongoCollection<GroupMessageDeleted> GroupMessageDeleteds =>
            Database.GetCollection<GroupMessageDeleted>("GroupMessageDeleteds");
        public IMongoCollection<RevokedTokens> RevokedTokens =>
            Database.GetCollection<RevokedTokens>("RevokedTokens");

        public IMongoCollection<RefreshToken> RefreshTokens =>
         Database.GetCollection<RefreshToken>("RefreshTokens");
        public IMongoCollection<EmailConfirmationToken> EmailConfirmationTokens =>
    Database.GetCollection<EmailConfirmationToken>("EmailConfirmationTokens");

        public IMongoCollection<PasswordResetToken> PasswordResetTokens =>
            Database.GetCollection<PasswordResetToken>("PasswordResetTokens");

        public IMongoCollection<Interest> Interests =>
           Database.GetCollection<Interest>("Interests");
        public IMongoCollection<User_group_Interest> User_group_Interests =>
         Database.GetCollection<User_group_Interest>("User_group_Interests");
        public IMongoCollection<RoutePiece> RoutePieces =>
     Database.GetCollection<RoutePiece>("RoutePieces");


        public IMongoCollection<Museum> Museums =>
          Database.GetCollection<Museum>("Museums");

        public IMongoCollection<Governorate> Governorates =>
            Database.GetCollection<Governorate>("Governorates");
        public IMongoCollection<ArtPiece> ArtPieces =>
            Database.GetCollection<ArtPiece>("ArtPieces");

        public IMongoCollection<Visit> Visits =>
            Database.GetCollection<Visit>("Visits");

        public IMongoCollection<Favourite> Favourites =>
            Database.GetCollection<Favourite>("Favourites");


        public IMongoCollection<Artifact> Artifacts =>
           Database.GetCollection<Artifact>("Artifacts");
        public IMongoCollection<Note> Notes =>
       Database.GetCollection<Note>("Notes");
    }
}
