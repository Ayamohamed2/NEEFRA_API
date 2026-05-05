using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using NEEFRA.Core.Entities.Common;

namespace NEEFRA.Core.Entities.Account
{
    public class ApplicationUser :BaseEntity
    {
       
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }


        public string PasswordHash { get; set; }

        public string Role { get; set; } = "Customer";

        public bool IsEmailConfirmed { get; set; } = false;

        public string ImageURL { get; set; } = "/Images/default.png";

        public DateTime LastSeen { get; set; } = DateTime.UtcNow;
        public int AccessFailedCount { get; set; } = 0;

        public DateTime? LockoutEnd { get; set; } = null;
        public string? Provider { get; set; }
        public string? ProviderId { get; set; }

    }
}
