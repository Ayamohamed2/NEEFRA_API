using NEEFRA.Core.DTO.Interests;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Entities.Inerests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.Interfaces.IService
{
    public interface IInterestService
    {
        Task<ServiceResult<List<Interest>>> GetIntersets();
        Task<ServiceResult<List<object>>> Get_U_G_Interests(string? GroupId, string? userId);
        Task<ServiceResult<object>> AddInterst(U_G_InterestDTO dto, string? userId);
        Task<ServiceResult<object>> DeleteInterst(U_G_InterestDTO dto, string? userId);
    }
}
