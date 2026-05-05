using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NEEFRA_API.Models
{
    public enum VisitType
    {
        Solo,
        Group
    }
    public class Visit
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string MuseumId { get; set; }

        public VisitType VisitType { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string? GroupId { get; set; }
        //public List<ViewedPiece> ViewedPieces { get; set; } = new();

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsInsideMuseum { get; set; } = false;
    }
    public class ViewedPiece
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string PieceId { get; set; }
        public string PieceName { get; set; }
    }
}
