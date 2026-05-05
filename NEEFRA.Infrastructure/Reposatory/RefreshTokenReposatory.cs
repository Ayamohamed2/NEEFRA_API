using NEEFRA_API.DataAccess.Data;
using Villa_API_Project.DataAccess.Reposatory.IReposatory;
using Villa_API_Project.Models;

namespace Villa_API_Project.DataAccess.Reposatory
{
    public class RefreshTokenReposatory:Reposatory<RefreshToken>,IRefreshTokenReposatory
    {
        MongoDbContext Context;
        public RefreshTokenReposatory(MongoDbContext context) : base(context)
        {
            this.Context = context;

        }


    }
}
