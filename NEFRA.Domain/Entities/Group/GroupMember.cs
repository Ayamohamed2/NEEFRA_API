using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using NEEFRA.Core.Entities.Common;

namespace NEEFRA.Core.Entities.Group
{
    public class GroupMember : BaseEntity
    {
  

        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string GroupId { get; set; }


        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }


        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        public bool IsAdmin { get; set; } = false;

        public bool IsActive { get; set; } = true;


    }
}
