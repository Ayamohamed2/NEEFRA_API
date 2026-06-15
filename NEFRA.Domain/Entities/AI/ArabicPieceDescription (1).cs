using NEEFRA.Core.Entities.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NEEFRA.Core.Entities
{
    public class ArabicPieceDescription : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
