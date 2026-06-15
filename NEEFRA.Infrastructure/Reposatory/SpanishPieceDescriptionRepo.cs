using NEEFRA.Core.Entities;
using NEEFRA.Core.Interfaces.IReposatory;
using NEEFRA_API.DataAccess.Data;
using Villa_API_Project.DataAccess.Reposatory;

namespace NEEFRA.Infrastructure.Reposatory
{
    public class SpanishPieceDescriptionRepo : Reposatory<SpanishPieceDescription>, ISpanishPieceDescriptionRepo
    {
        MongoDbContext Context;
        public SpanishPieceDescriptionRepo(MongoDbContext context) : base(context)
        {
            this.Context = context;
        }
    }
}
