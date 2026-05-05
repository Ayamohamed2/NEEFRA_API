using NEEFRA_API.DTO;
using NEEFRA.Core.DTO.Service;

namespace NEEFRA.Core.Interfaces.IService
{
    public interface IFavouriteService
    {
        Task<ServiceResult<FavouriteDTO>> AddFavouriteAsync(string userId, AddFavouriteDTO dto);
        Task<ServiceResult<List<FavouriteDTO>>> GetMyFavouritesAsync(string userId);
    }
}
