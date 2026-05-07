namespace NEEFRA_API.DTO
{
    public class NearbyHotelDTO
    {
        public string Id { get; set; }
        public string MuseumId { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double DistanceInKm { get; set; }
        public int? StarRating { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Website { get; set; }
        public string? PhotoUrl { get; set; }
    }

    public class CreateNearbyHotelDTO
    {
        public string MuseumId { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double DistanceInKm { get; set; }
        public int? StarRating { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Website { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
