using MongoDB.Driver;
using NEEFRA.Domain.IReposatory;
using NEEFRA_API.DataAccess.Data;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using NEEFRA_API.Models;
using Villa_API_Project.DataAccess.Reposatory;

namespace NEEFRA_API.DataAccess.Reposatory
{
    public class NoteRepository : Reposatory<Note>,INoteRepository
    {
        private readonly IMongoCollection<Note> _notes;

        public NoteRepository(MongoDbContext context):base(context)
        {
            _notes = context.Notes;
        }

        // إضافة Note جديدة
        public async Task<Note> AddAsync(Note note)
        {
            await _notes.InsertOneAsync(note);
            return note;
        }

        // جيب كل Notes اليوزر
        public async Task<List<Note>> GetUserNotesAsync(string userId)
        {
            return await _notes
                .Find(n => n.UserId == userId)
                .ToListAsync();
        }

        // جيب Note بالـ Id
        public async Task<Note?> GetByIdAsync(string noteId)
        {
            return await _notes
                .Find(n => n.Id == noteId)
                .FirstOrDefaultAsync();
        }

        // تعديل Note
        public async Task<Note?> UpdateAsync(string noteId, string newContent)
        {
            var update = Builders<Note>.Update
                .Set(n => n.Content, newContent)
                .Set(n => n.UpdatedAt, DateTime.UtcNow);

            var options = new FindOneAndUpdateOptions<Note>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await _notes.FindOneAndUpdateAsync(
                n => n.Id == noteId,
                update,
                options
            );
        }

        // حذف Note
        public async Task<bool> DeleteAsync(string noteId)
        {
            var result = await _notes.DeleteOneAsync(n => n.Id == noteId);
            return result.DeletedCount > 0;
        }
    }
}

