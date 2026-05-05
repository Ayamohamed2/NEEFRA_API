using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NEEFRA.Core.Entities.Common;

namespace NEEFRA_API.Models
{
    public class Favourite: BaseEntity
    {
 
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ArtPieceId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string VisitId { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
