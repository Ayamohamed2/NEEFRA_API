using NEEFRA.Domain.IReposatory;
using NEEFRA_API.Models;

namespace NEEFRA_API.DataAccess.Reposatory.IReposatory
{
    public interface INoteRepository:IReposatory<Note>
    {
        Task<Note> AddAsync(Note note);
        Task<List<Note>> GetUserNotesAsync(string userId);
        Task<Note?> GetByIdAsync(string noteId);
        Task<Note?> UpdateAsync(string noteId, string newContent);
        Task<bool> DeleteAsync(string noteId);
    }
}
