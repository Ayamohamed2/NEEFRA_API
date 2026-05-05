using NEEFRA_API.DTO;
using NEEFRA.Core.DTO.Service;

namespace NEEFRA.Core.Interfaces.IService
{
    public interface INoteService
    {
        Task<ServiceResult<NoteDTO>> AddNoteAsync(string userId, AddNoteDTO dto);
        Task<ServiceResult<List<NoteDTO>>> GetMyNotesAsync(string userId);
        Task<ServiceResult<NoteDTO>> UpdateNoteAsync(string userId, string noteId, UpdateNoteDTO dto);
        Task<ServiceResult<object>> DeleteNoteAsync(string userId, string noteId);
    }
}
