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
    public class InterestRepo :Reposatory<Interest>,IinterestRepo
    {
        MongoDbContext Context;
        public InterestRepo(MongoDbContext context) : base(context)
        {
            this.Context = context;

        }
    }
}
