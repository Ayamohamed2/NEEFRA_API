using NEEFRA.Core.Entities;
using NEEFRA.Domain.IReposatory;

namespace NEEFRA_API.DataAccess.Reposatory.IReposatory
{
    public interface IMuseumRepository: IReposatory<Museum>
    {
        Task<List<Museum>> GetAllMuseumsAsync();
        Task<List<Museum>> GetMuseumsByGovernorateAsync(string governorateId);
        Task<Museum?> GetByIdAsync(string museumId);
        Task<Museum> AddMuseumAsync(Museum museum);
    }
}
