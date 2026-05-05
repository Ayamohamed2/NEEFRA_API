namespace NEEFRA.Core.DTO.Group
{
    public class MessageReadInfoDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string? UserImage { get; set; }
        public DateTime ReadAt { get; set; }
    }
}
