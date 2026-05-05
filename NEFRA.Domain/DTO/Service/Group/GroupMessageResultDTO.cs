using NEEFRA.Core.Entities.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.DTO.Service.Group
{
    public class GroupMessageResultDTO
    {
        public string Id { get; set; } = default!;
        public string GroupId { get; set; } = default!;
        public string SenderId { get; set; } = default!;
        public string? SenderName { get; set; }
        public string? SenderImage { get; set; }
        public MessageType Type { get; set; }
        public string? TextContent { get; set; }
        public string? MediaUrl { get; set; }
        public double? MediaDuration { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsForwarded { get; set; }
        public string? ReplyToMessageId { get; set; }
        public string? ReplyToText { get; set; }
        public string? ReplyToSenderName { get; set; }
        public MessageType? ReplyToType { get; set; }
        public string? ReplyToMediaUrl { get; set; }
        public int TotalMembers { get; set; }
        public int ReadByCount { get; set; }
        public bool ReadByMe { get; set; }
        public bool ReadByAll { get; set; }
    }
}
