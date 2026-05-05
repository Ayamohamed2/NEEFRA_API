using System.ComponentModel.DataAnnotations;

namespace NEEFRA.Core.DTO.Email
{
    public class EmailDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
