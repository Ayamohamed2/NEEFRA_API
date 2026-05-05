using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using NEEFRA.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.Entities.Route
{
    public class RoutePiece:BaseEntity
    {
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]

        public string UserId { get; set; }
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]

        public string VisitId { get; set; }
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]

        public string PieceId { get; set; }
        [Required]

        public string PieceName { get; set; }
        [Required]

        public int VisitOrder { get; set; }
         public string? ImageURl { get; set; }
        public bool Visited { get; set; } = false;
        public DateTime VisitAt { get; set; } = DateTime.UtcNow;
    }
}
