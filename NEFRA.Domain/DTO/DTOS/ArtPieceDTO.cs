namespace NEEFRA_API.DTO
{
    public class ArtPieceDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string MuseumId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Floor { get; set; }
    }
    public class CreateArtPieceDTO
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string MuseumId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Floor { get; set; }
    }
}
