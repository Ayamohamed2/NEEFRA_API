using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using NEEFRA.Core.Entities.Common;

namespace NEEFRA.Core.Entities.Group
{
    public class Group : BaseEntity
    {
    

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public int NumberOf_members { get; set; }

        public string tour_type { get; set; }
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CreatorId { get; set; }


        [Required]
        public string JoinCode { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}
