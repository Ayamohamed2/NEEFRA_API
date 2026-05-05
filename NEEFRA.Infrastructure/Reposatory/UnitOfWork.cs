using NEEFRA.Core.Interfaces.IReposatory;
using NEEFRA.Domain.IReposatory;
using NEEFRA.Infrastructure.Reposatory;
using NEEFRA_API.DataAccess.Data;
using NEEFRA_API.DataAccess.Reposatory;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using Realtima_Chat_project.DataAccess.Reposatory;
using Villa_API_Project.DataAccess.Reposatory.IReposatory;



namespace Villa_API_Project.DataAccess.Reposatory
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly MongoDbContext _context;


        public IAPPlicationUserReposatory Users { get; private set; }


        public IRevokedTokensReposatory RevokedTokens { get; private set; }

        public IRefreshTokenReposatory RefreshToken { get; private set; }

        public IEmailTokensReposatory EmailTokens { get; private set; }
        public IPasswordResetTokensReposatory PasswordResetTokens { get; private set; }

        public IGroupReposatory Group { get; private set; }

        public IGroupMemberReposatory GroupMember { get; private set; }

        public IGroupMessagesReposatory GroupMessages { get; private set; }
        
        public IGroupMessageReadRepo GroupMessageRead { get; private set; }

        public IGroupMessageDeletedRepo GroupMessageDeleted { get; private set; }

        public IinterestRepo Interests { get; private set; }

        public IUser_group_InterestRepo U_G_Interest { get; private set; }

        public IRoutePieceRepo RoutePiece { get; private set; }

        public IVisitRepository Visit { get; private set; }

        public IGovernorateRepository Governorate { get; private set; }

        public IMuseumRepository Museum { get; private set; }

        public IFavouriteRepository Favourite { get; private set; }

        public IArtPieceRepository ArtPiece { get; private set; }

        public IArtifcatRepo Artifcat { get; private set; }

        public UnitOfWork(MongoDbContext context)
        {
            _context = context;

            Users = new ApplicationUserReposatory(context);
            RevokedTokens = new RevokedTokensReposatory(context);
            Group = new GroupRepo(context);
            GroupMember = new GroupMemberRepo(context);
            GroupMessages = new GroupMessagesRepo(context);
            GroupMessageRead = new GRoupMessageRead(context);
            GroupMessageDeleted = new GroupMessageDeletedRepo(context);

            EmailTokens = new EmailTokensReposatory(context);
            PasswordResetTokens = new PasswordResetTokensReposatory(context);
            Interests = new InterestRepo(context);
            U_G_Interest = new U_G_InterestRepo(context);

            RoutePiece = new RoutePieceRepo(context);


            Visit = new VisitRepository(context);

            Governorate = new GovernorateRepository(context);

            Museum = new MuseumRepository(context);

            Favourite = new FavouriteRepository(context);

            ArtPiece = new ArtPieceRepository(context);

            RefreshToken = new RefreshTokenReposatory(context);
            Artifcat = new ArtifactRepo(context);
        }


    }
}
