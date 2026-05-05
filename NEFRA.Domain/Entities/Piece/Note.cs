using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace NEEFRA_API.Models
{
    public class Note
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ArtPieceId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? VisitId { get; set; }

        public string Content { get; set; }  // نص الملاحظة

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
