using NEEFRA.Core.Entities.Inerests;
using NEEFRA.Core.Interfaces.IReposatory;
using NEEFRA_API.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villa_API_Project.DataAccess.Reposatory;

namespace NEEFRA.Infrastructure.Reposatory
{
    public class U_G_InterestRepo :Reposatory<User_group_Interest>,IUser_group_InterestRepo
    {
        MongoDbContext Context;
        public U_G_InterestRepo(MongoDbContext context) : base(context)
        {
            this.Context = context;

        }
    }
}
