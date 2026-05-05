using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using NEEFRA.Core.Entities.Common;

namespace NEEFRA.Core.Entities.Group
{
    public class GroupMessage : BaseEntity
    {
       

        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string GroupId { get; set; }



        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SenderId { get; set; }



        public MessageType Type { get; set; } = MessageType.Text;

        public string? TextContent { get; set; }


        public string? MediaUrl { get; set; }

        public int? MediaDuration { get; set; }

        public DateTime CreatedAt { get; set; } 



        [BsonRepresentation(BsonType.ObjectId)]
        public string? ReplyToMessageId { get; set; }


    }

    public enum MessageType
    {
        Text = 0,
        Image = 1,
        Video = 2,
        Voice = 3,
    }
}

