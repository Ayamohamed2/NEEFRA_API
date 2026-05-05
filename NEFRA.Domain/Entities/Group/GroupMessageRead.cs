using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using NEEFRA.Core.Entities.Common;

namespace NEEFRA.Core.Entities.Group
{
    public class GroupMessageRead : BaseEntity
    {
     

        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MessageId { get; set; }


        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }



        public DateTime ReadAt { get; set; } = DateTime.UtcNow;
    }
}
