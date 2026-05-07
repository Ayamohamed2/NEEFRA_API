using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NEEFRA_API.Models
{
    public class GovernoratePhoto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string GovernorateId { get; set; } = null!;

        public string PhotoUrl { get; set; } = null!;
        public string? Caption { get; set; }
        public bool IsPrimary { get; set; } = false;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
