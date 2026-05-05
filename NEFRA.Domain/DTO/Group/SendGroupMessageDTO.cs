using Microsoft.AspNetCore.Http;
using NEEFRA.Core.Entities.Group;

namespace NEEFRA.Core.DTO.Group
{
    public class SendGroupMessageDTO
    {
        public string GroupId { get; set; }
        public string? Text { get; set; }
        public IFormFile? File { get; set; }
        public MessageType Type { get; set; } = MessageType.Text;
        public int? MediaDuration { get; set; }
    }
}
