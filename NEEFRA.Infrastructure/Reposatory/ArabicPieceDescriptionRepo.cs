using NEEFRA.Core.Entities;
using NEEFRA.Core.Interfaces.IReposatory;
using NEEFRA_API.DataAccess.Data;
using Villa_API_Project.DataAccess.Reposatory;

namespace NEEFRA.Infrastructure.Reposatory
{
    public class ArabicPieceDescriptionRepo : Reposatory<ArabicPieceDescription>, IArabicPieceDescriptionRepo
    {
        MongoDbContext Context;
        public ArabicPieceDescriptionRepo(MongoDbContext context) : base(context)
        {
            this.Context = context;
        }
    }
}
