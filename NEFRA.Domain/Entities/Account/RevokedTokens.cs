using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using NEEFRA.Core.Entities.Common;

namespace NEEFRA.Core.Entities.Account
{
    public class RevokedTokens : BaseEntity
    {


        public string Token { get; set; } = string.Empty;

        public DateTime ExpiredAt { get; set; } = DateTime.UtcNow;
    }
}
