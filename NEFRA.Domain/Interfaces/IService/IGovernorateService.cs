using NEEFRA_API.DTO;
using NEEFRA.Core.DTO.Service;

namespace NEEFRA.Core.Interfaces.IService
{
    public interface IGovernorateService
    {
        Task<ServiceResult<List<GovernorateDTO>>> GetAllAsync();
        Task<ServiceResult<GovernorateDTO>> AddAsync(CreateGovernorateDTO dto);
    }
}
