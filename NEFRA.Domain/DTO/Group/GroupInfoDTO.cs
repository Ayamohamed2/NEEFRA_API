namespace NEEFRA.Core.DTO.Group
{
    public class GroupInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int NumberOf_members { get; set; }

        public string tour_type { get; set; } = string.Empty;
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public string JoinCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public int MembersCount { get; set; }
        public int UnreadCount { get; set; }
        public GroupMessageDTO? LastMessage { get; set; }
        public bool IsAdmin { get; set; }
    }
}
