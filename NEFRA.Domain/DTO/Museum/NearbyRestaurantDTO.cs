namespace NEEFRA_API.DTO
{
    public class NearbyRestaurantDTO
    {
        public string Id { get; set; }
        public string MuseumId { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double DistanceInKm { get; set; }
        public string? CuisineType { get; set; }
        public string? PriceRange { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Website { get; set; }
        public string? PhotoUrl { get; set; }
    }

    public class CreateNearbyRestaurantDTO
    {
        public string MuseumId { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double DistanceInKm { get; set; }
        public string? CuisineType { get; set; }
        public string? PriceRange { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Website { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
