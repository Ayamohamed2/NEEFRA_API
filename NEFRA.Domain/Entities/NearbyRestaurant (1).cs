using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NEEFRA_API.Models
{
    public class NearbyRestaurant
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string MuseumId { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double DistanceInKm { get; set; }
        public string? CuisineType { get; set; }
        public string? PriceRange { get; set; }   // e.g. "$", "$$", "$$$"
        public string? PhoneNumber { get; set; }
        public string? Website { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
