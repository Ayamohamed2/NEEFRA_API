using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NEEFRA.Core.DTO.Account
{
    public class ResetPasswordDTO
    {

        [Required]

        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]

        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
