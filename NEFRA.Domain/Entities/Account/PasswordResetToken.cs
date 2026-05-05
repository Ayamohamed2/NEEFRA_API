using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using NEEFRA.Core.Entities.Common;

namespace NEEFRA.Core.Entities.Account
{
    public class PasswordResetToken : BaseEntity
    {

       

        public string UserId { get; set; }

        public string Token { get; set; }

        public DateTime ExpireAt { get; set; }

        public bool IsUsed { get; set; } = false;
    }
}
