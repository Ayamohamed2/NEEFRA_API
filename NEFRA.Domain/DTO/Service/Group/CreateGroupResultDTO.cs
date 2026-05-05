using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.DTO.Service.Group
{
    public class CreateGroupResultDTO
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string JoinCode { get; set; } = default!;
        public string? CreatorName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
