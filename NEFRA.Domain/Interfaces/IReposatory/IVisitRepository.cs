using NEEFRA.Domain.IReposatory;
using NEEFRA_API.Models;

namespace NEEFRA_API.DataAccess.Reposatory.IReposatory
{
    public interface IVisitRepository :IReposatory<Visit>
    {
        Task<Visit> StartVisitAsync(Visit visit);
        Task<Visit?> GetVisitByIdAsync(string visitId);
        Task<Visit> EndVisitAsync(string visitId);
        Task<List<Visit>> GetUserVisitsAsync(string userId);
        Task<Visit?> GetActiveVisitAsync(string userId, string museumId); // ✅ جديد
        Task<List<Visit>> GetUserVisitsByTypeAsync(string userId, VisitType visitType);
    }
}
