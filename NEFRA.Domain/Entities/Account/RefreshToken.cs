using NEEFRA.Core.Entities.Common;

namespace Villa_API_Project.Models
{
    public class RefreshToken: BaseEntity
    {
      
        public string UserId { get; set; }
        public string JwtTokenId { get; set; }
        public string Refresh_Token { get; set; }
        public bool IsValid { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
