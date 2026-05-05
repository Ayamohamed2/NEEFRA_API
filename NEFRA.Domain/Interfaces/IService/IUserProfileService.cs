using Microsoft.AspNetCore.Hosting;
using NEEFRA.Core.DTO.Profile;
using NEEFRA.Core.DTO.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.Interfaces.IService
{
    public interface IUserProfileService
    {
        Task<ServiceResult<UserProfileResponseDTO>> GetProfileAsync(string userId, string baseUrl);
        Task<ServiceResult<UserProfileResponseDTO>> UpdateProfileAsync(string userId, UserProfileDTO profileDTO, string baseUrl, IWebHostEnvironment env);
    }
}
