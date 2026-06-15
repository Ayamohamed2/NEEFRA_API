using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.DTO.AIDescription
{
    public class AIDescriptionDTO
    {
        [Required]
        public string VisitId { get; set; }

        public string? lang { get; set; }
        public string? type { get; set; }
        public string? PieceName { get; set; }
    }
}
