using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NEEFRA_API.Models
{
    public class Cafe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string MuseumId { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? OpenHours { get; set; }
        public string? Location { get; set; }   // e.g. "Rooftop", "Garden Area"
        public string? PhotoUrl { get; set; }
        public bool HasOutdoorSeating { get; set; }
    }
}
