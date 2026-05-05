using Microsoft.AspNetCore.Http;

namespace NEEFRA.Core.DTO.Group
{
    public class CreateGroupDTO
    {

        public string Name { get; set; }
        public string? Description { get; set; }
        public int NumberOf_members { get; set; }

        public string tour_type { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
    }
}
