using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NEEFRA.Core.Entities.Common;
namespace NEEFRA.Core.Entities
{
    public class Museum : BaseEntity
    {


        public string Name { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string GeneralInfo { get; set; }
        public string Open_Hours { get; set; }
        public decimal TicketEgyptAdultPrice { get; set; }
        public decimal TicketEgyptStudentPrice { get; set; }
        public decimal TicketForienerAdultPrice { get; set; }
        public decimal TicketForienerStudentPrice { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string GovernorateId { get; set; }

        [BsonIgnore]
        public Governorate? Governorate { get; set; }
    }
}

