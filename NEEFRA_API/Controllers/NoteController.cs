using Microsoft.AspNetCore.Mvc;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DTO;
using Restaurant.API.Controllers;

namespace NEEFRA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : BaseController
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        // POST: api/Note
        [HttpPost]
        public async Task<IActionResult> AddNote([FromBody] AddNoteDTO dto)
        {
            var result = await _noteService.AddNoteAsync(UserId, dto);
            return HandleResult(result);
        }

        // GET: api/Note
        [HttpGet]
        public async Task<IActionResult> GetMyNotes()
        {
            var result = await _noteService.GetMyNotesAsync(UserId);
            return HandleResult(result);
        }

        // PUT: api/Note/{noteId}
        [HttpPut("{noteId}")]
        public async Task<IActionResult> UpdateNote(string noteId, [FromBody] UpdateNoteDTO dto)
        {
            var result = await _noteService.UpdateNoteAsync(UserId, noteId, dto);
            return HandleResult(result);
        }

        // DELETE: api/Note/{noteId}
        [HttpDelete("{noteId}")]
        public async Task<IActionResult> DeleteNote(string noteId)
        {
            var result = await _noteService.DeleteNoteAsync(UserId, noteId);
            return HandleResult(result);
        }
    }
}
