using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace NEEFRA.Core.DTO.Profile
{
    public class UserProfileDTO
    {
        public string Name { get; set; }
        [ValidateNever]
        public IFormFile? imagefile { get; set; }
    }
}
