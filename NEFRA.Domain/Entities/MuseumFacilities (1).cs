using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NEEFRA_API.Models
{
    public class MuseumFacilities
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string MuseumId { get; set; } = null!;

        public bool HasWifi { get; set; }
        public bool IsWheelchairAccessible { get; set; }
        public bool HasAudioGuide { get; set; }
        public bool HasLockers { get; set; }
        public string? WifiPassword { get; set; }
        public string? AudioGuideLanguages { get; set; }
        public string? Notes { get; set; }


        public string? ImageUrl { get; set; }
    }
}
