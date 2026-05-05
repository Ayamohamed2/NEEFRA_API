namespace NEEFRA_API.DTO
{
    public class FavouriteDTO
    {
        public string Id { get; set; }
        public string ArtPieceId { get; set; }
        public string VisitId { get; set; }
        public DateTime AddedAt { get; set; }
    }
    public class AddFavouriteDTO
    {
        public string ArtPieceId { get; set; }
        public string VisitId { get; set; }
    }
}
