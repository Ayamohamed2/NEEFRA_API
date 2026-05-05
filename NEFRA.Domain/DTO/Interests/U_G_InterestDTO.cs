using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.DTO.Interests
{
    public class U_G_InterestDTO
    {

        public string? GroupId { get; set; } 
        [Required]
        public string InterestId { get; set; }

    }
}
