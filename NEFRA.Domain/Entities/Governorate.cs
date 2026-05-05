using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace NEEFRA.Core.Entities
{
    public class Governorate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public required string Name { get; set; }
        public string Capital { get; set; }
        public string Region { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
