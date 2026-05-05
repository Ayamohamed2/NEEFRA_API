using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NEEFRA.Core.Entities.Common;

namespace NEEFRA_API.Models
{
    public class ArtPiece: BaseEntity
    {


        public string Name { get; set; }

        public string ImageUrl { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string MuseumId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Floor { get; set; }
        public bool Valid { get; set; }
    }
}
