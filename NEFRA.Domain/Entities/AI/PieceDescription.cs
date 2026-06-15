using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NEEFRA.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.Entities.Pieces
{
    public class PieceDescription: BaseEntity
    {
       

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
