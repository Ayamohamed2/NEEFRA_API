namespace NEEFRA_API.DTO
{
    public class AddNoteDTO
    {
        public string ArtPieceId { get; set; }
        public string? VisitId { get; set; }
        public string Content { get; set; }
    }

    // request — لما بيعدل الملاحظة
    public class UpdateNoteDTO
    {
        public string Content { get; set; }
    }

    // response — اللي بيرجع لليوزر
    public class NoteDTO
    {
        public string Id { get; set; }
        public string ArtPieceId { get; set; }
        public string? VisitId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
