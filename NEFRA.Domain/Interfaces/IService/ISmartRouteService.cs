using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.Interfaces.IService
{
    public interface ISmartRouteService
    {
        Task<ServiceResult<List<RouteArtPieceDTO>>> GetRouteAsync(
    string userId,
    string? groupId);
    }
}
