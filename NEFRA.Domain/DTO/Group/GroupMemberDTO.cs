namespace NEEFRA.Core.DTO.Group
{
    public class GroupMemberDTO
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOnline { get; set; }
    }
}
