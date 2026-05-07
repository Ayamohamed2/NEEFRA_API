namespace NEEFRA_API.DTO
{
    public class CafeDTO
    {
        public string Id { get; set; }
        public string MuseumId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? OpenHours { get; set; }
        public string? Location { get; set; }
        public string? PhotoUrl { get; set; }
        public bool HasOutdoorSeating { get; set; }
    }

    public class CreateCafeDTO
    {
        public string MuseumId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? OpenHours { get; set; }
        public string? Location { get; set; }
        public string? PhotoUrl { get; set; }
        public bool HasOutdoorSeating { get; set; }
    }
}
