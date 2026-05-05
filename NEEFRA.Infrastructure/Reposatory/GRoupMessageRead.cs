using NEEFRA.Core.Entities.Group;
using NEEFRA.Domain.IReposatory;
using NEEFRA_API.DataAccess.Data;
using Villa_API_Project.DataAccess.Reposatory;

namespace Realtima_Chat_project.DataAccess.Reposatory
{
    public class GRoupMessageRead : Reposatory<GroupMessageRead>, IGroupMessageReadRepo
    {
        MongoDbContext Context;
        public GRoupMessageRead(MongoDbContext context) : base(context)
        {
            this.Context = context;

        }
    }
}
