using NEEFRA.Core.Entities.Group;
using NEEFRA.Domain.IReposatory;
using NEEFRA_API.DataAccess.Data;
using Villa_API_Project.DataAccess.Reposatory;

namespace Realtima_Chat_project.DataAccess.Reposatory
{
    public class GroupMessageDeletedRepo :Reposatory<GroupMessageDeleted>, IGroupMessageDeletedRepo
    {
        MongoDbContext Context;
        public GroupMessageDeletedRepo(MongoDbContext context) : base(context)
        {
            this.Context = context;

        }
    }
}
