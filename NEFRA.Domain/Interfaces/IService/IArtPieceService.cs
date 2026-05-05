using NEEFRA_API.DTO;
using NEEFRA.Core.DTO.Service;

namespace NEEFRA.Core.Interfaces.IService
{
    public interface IArtPieceService
    {
        Task<ServiceResult<List<ArtPieceDTO>>> GetByMuseumAsync(string museumId);
        Task<ServiceResult<ArtPieceDTO>> AddAsync(CreateArtPieceDTO dto);
    }
}
