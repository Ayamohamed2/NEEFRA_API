using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.DTO.Service.Group
{
    public class MarkAsReadResultDTO
    {
        public string MessageId { get; set; } = default!;
        public string GroupId { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string? UserName { get; set; }
        public DateTime ReadAt { get; set; }
        public bool AlreadyRead { get; set; }
    }
}
