using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.DTO.Service.Group
{
    public class GroupDataDTO
    {
        public string GroupId { get; set; } = default!;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? TourType { get; set; }
        public int membersCount { get; set; }
        public string? ImageUrl { get; set; }

        public string joinCode { get; set; }
        public int Expexted_numberOfmembers { get; set; }

        public DateTime createdAt { get; set; }
    }
}
