using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.Entities.Piece
{
    public class Artifact
    {
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        public string Name { get; set; } = string.Empty;        
        public List<string> Interests { get; set; } = new();    
    }
}
