using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.DTO.Service.Group
{
    public class UpdateGroupResultDTO
    {
        public string GroupId { get; set; } = default!;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? TourType { get; set; }
        public int NumberOfMembers { get; set; }
        public string? ImageUrl { get; set; }
    }
}
