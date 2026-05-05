using NEEFRA_API.DTO;
using NEEFRA.Core.DTO.Service;

namespace NEEFRA.Core.Interfaces.IService
{
    public interface IVisitService
    {
        Task<ServiceResult<object>> CheckAndStartAsync(string userId, CheckLocationDTO dto);
        Task<ServiceResult<EndVisitDTO>> EndVisitAsync(string userId, string visitId);
        Task<ServiceResult<List<VisitDTO>>> GetMyVisitsAsync(string userId);
        Task<ServiceResult<List<VisitDTO>>> GetMySoloVisitsAsync(string userId);
        Task<ServiceResult<List<VisitDTO>>> GetMyGroupVisitsAsync(string userId);
    }
}
