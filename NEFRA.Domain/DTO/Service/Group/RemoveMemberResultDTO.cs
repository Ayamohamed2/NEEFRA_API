using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.DTO.Service.Group
{
    public class RemoveMemberResultDTO
    {
        public string GroupId { get; set; } = default!;
        public string? GroupName { get; set; }
        public string RemovedUserId { get; set; } = default!;
        public string? RemovedUserName { get; set; }
    }
}
