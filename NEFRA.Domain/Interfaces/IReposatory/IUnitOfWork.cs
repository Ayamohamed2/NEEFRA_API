using NEEFRA.Core.Interfaces.IReposatory;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using Villa_API_Project.DataAccess.Reposatory.IReposatory;

namespace NEEFRA.Domain.IReposatory
{
    public interface IUnitOfWork
    {


        public IAPPlicationUserReposatory Users { get; }

        public IRevokedTokensReposatory RevokedTokens { get; }

        public IRefreshTokenReposatory RefreshToken { get; }

        public IGroupReposatory Group { get; }
        public IGroupMemberReposatory GroupMember { get; }
        public IGroupMessagesReposatory GroupMessages { get; }
        public IGroupMessageReadRepo GroupMessageRead { get; }

        public IGroupMessageDeletedRepo GroupMessageDeleted { get; }

        public IEmailTokensReposatory EmailTokens { get; }
        public IPasswordResetTokensReposatory PasswordResetTokens { get; }
        public IinterestRepo Interests { get; }
        public IUser_group_InterestRepo U_G_Interest { get; }
        public IRoutePieceRepo RoutePiece { get; }



        public IVisitRepository Visit { get; }

        public IGovernorateRepository Governorate { get; }

        public IMuseumRepository Museum { get; }

        public IFavouriteRepository Favourite { get; }

        public IArtPieceRepository ArtPiece { get; }

        public IArtifcatRepo Artifcat { get; }






    }
}
