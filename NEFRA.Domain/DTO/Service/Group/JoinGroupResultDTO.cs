using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.DTO.Service.Group
{
    public class JoinGroupResultDTO
    {
        public string GroupId { get; set; } = default!;
        public string GroupName { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string? UserName { get; set; }
        public string? UserImage { get; set; }
        public DateTime JoinedAt { get; set; }
    }

}
