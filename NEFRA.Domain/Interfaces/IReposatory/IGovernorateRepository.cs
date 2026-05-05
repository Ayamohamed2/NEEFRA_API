using NEEFRA.Core.Entities;
using NEEFRA.Domain.IReposatory;

namespace NEEFRA_API.DataAccess.Reposatory.IReposatory
{
    public interface IGovernorateRepository :IReposatory<Governorate>
    {
        Task<List<Governorate>> GetAllAsync();
        Task<Governorate> AddAsync(Governorate governorate);
    }
}
