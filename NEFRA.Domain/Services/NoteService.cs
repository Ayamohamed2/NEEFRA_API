using Microsoft.Extensions.Logging;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using NEEFRA_API.DTO;
using NEEFRA_API.Models;

namespace NEEFRA.Core.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepo;
        private readonly ILogger<NoteService> _logger;

        public NoteService(INoteRepository noteRepo, ILogger<NoteService> logger)
        {
            _noteRepo = noteRepo;
            _logger = logger;
        }

        // ════════════════════════════════════════════════════════════════════
        // Note Operations
        // ════════════════════════════════════════════════════════════════════

        public async Task<ServiceResult<NoteDTO>> AddNoteAsync(string userId, AddNoteDTO dto)
        {
            _logger.LogInformation("Adding note – userId: {UserId}, artPieceId: {ArtPieceId}", userId, dto.ArtPieceId);

            var note = new Note
            {
                UserId = userId,
                ArtPieceId = dto.ArtPieceId,
                VisitId = dto.VisitId,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _noteRepo.AddAsync(note);

            _logger.LogInformation("Note added – id: {Id}, userId: {UserId}", created.Id, userId);

            return new()
            {
                IsSuccess = true,
                Message = "Note added successfully",
                Data = MapToDTO(created)
            };
        }

        public async Task<ServiceResult<List<NoteDTO>>> GetMyNotesAsync(string userId)
        {
            _logger.LogInformation("Fetching notes for userId: {UserId}", userId);

            var notes = await _noteRepo.GetUserNotesAsync(userId);

            var data = notes.Select(n => MapToDTO(n)).ToList();

            _logger.LogInformation("Fetched {Count} notes for userId: {UserId}", data.Count, userId);

            return new() { IsSuccess = true, Data = data };
        }

        public async Task<ServiceResult<NoteDTO>> UpdateNoteAsync(string userId, string noteId, UpdateNoteDTO dto)
        {
            _logger.LogInformation("Updating note – noteId: {NoteId}, userId: {UserId}", noteId, userId);

            var note = await _noteRepo.GetByIdAsync(noteId);
            if (note == null)
            {
                _logger.LogWarning("Update note failed – not found: {NoteId}", noteId);
                return new() { IsSuccess = false, Message = "Note not found", ErrorType = "NotFound" };
            }

            if (note.UserId != userId)
            {
                _logger.LogWarning("Update note forbidden – noteId: {NoteId}, requestingUserId: {UserId}", noteId, userId);
                return new() { IsSuccess = false, Message = "You are not authorized to update this note", ErrorType = "Forbidden" };
            }

            var updated = await _noteRepo.UpdateAsync(noteId, dto.Content);

            _logger.LogInformation("Note updated – noteId: {NoteId}", noteId);

            return new()
            {
                IsSuccess = true,
                Message = "Note updated successfully",
                Data = MapToDTO(updated!)
            };
        }

        public async Task<ServiceResult<object>> DeleteNoteAsync(string userId, string noteId)
        {
            _logger.LogInformation("Deleting note – noteId: {NoteId}, userId: {UserId}", noteId, userId);

            var note = await _noteRepo.GetByIdAsync(noteId);
            if (note == null)
            {
                _logger.LogWarning("Delete note failed – not found: {NoteId}", noteId);
                return new() { IsSuccess = false, Message = "Note not found", ErrorType = "NotFound" };
            }

            if (note.UserId != userId)
            {
                _logger.LogWarning("Delete note forbidden – noteId: {NoteId}, requestingUserId: {UserId}", noteId, userId);
                return new() { IsSuccess = false, Message = "You are not authorized to delete this note", ErrorType = "Forbidden" };
            }

            await _noteRepo.DeleteAsync(noteId);

            _logger.LogInformation("Note deleted – noteId: {NoteId}", noteId);

            return new() { IsSuccess = true, Message = "Note deleted successfully", Data = new { message = "Note deleted successfully" } };
        }

        // ════════════════════════════════════════════════════════════════════
        // Private helpers
        // ════════════════════════════════════════════════════════════════════

        private NoteDTO MapToDTO(Note note) => new NoteDTO
        {
            Id = note.Id,
            ArtPieceId = note.ArtPieceId,
            VisitId = note.VisitId,
            Content = note.Content,
            CreatedAt = note.CreatedAt,
            UpdatedAt = note.UpdatedAt
        };
    }
}
