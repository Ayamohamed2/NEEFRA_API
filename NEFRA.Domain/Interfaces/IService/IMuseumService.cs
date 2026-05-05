using NEEFRA_API.DTO;
using NEEFRA.Core.DTO.Service;

namespace NEEFRA.Core.Interfaces.IService
{
    public interface IMuseumService
    {
        Task<ServiceResult<List<MuseumDTO>>> GetAllAsync();
        Task<ServiceResult<List<MuseumDTO>>> GetByGovernorateAsync(string governorateId);
        Task<ServiceResult<MuseumDTO>> AddAsync(CreateMuseumDTO dto);
    }
}
