using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using NEEFRA.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.Entities.Inerests
{
    public class User_group_Interest :BaseEntity
    {
        public string? GroupId { get; set; }


        public string? UserId { get; set; } 

        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Interest_Id { get; set; }

    }
}
