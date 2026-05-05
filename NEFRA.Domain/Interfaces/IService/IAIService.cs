using NEEFRA.Core.DTO.AIDescription;
using NEEFRA.Core.DTO.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.Interfaces.IService
{
    public interface IAIService
    {
        Task<ServiceResult<object>> AIDescription(AIDescriptionDTO dto, string userId);
    }
}
