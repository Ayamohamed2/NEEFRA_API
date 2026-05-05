using MongoDB.Driver;
using NEEFRA.Core.Entities.Piece;
using NEEFRA.Core.Interfaces.IReposatory;
using NEEFRA_API.DataAccess.Data;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using NEEFRA_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villa_API_Project.DataAccess.Reposatory;

namespace NEEFRA.Infrastructure.Reposatory
{
    public class ArtifactRepo : Reposatory<Artifact>, IArtifcatRepo
    {
        private readonly IMongoCollection<ArtPiece> _artPieces;

        public ArtifactRepo(MongoDbContext context) : base(context)
        {
            _artPieces = context.ArtPieces;
        }
    }
}
