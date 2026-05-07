namespace NEEFRA_API.DTO
{
    public class MuseumFacilitiesDTO
    {
        public string Id { get; set; }
        public string MuseumId { get; set; }
        public bool HasWifi { get; set; }
        public bool IsWheelchairAccessible { get; set; }
        public bool HasAudioGuide { get; set; }
        public bool HasLockers { get; set; }
        public string? WifiPassword { get; set; }
        public string? AudioGuideLanguages { get; set; }
        public string? Notes { get; set; }
    }

    public class CreateMuseumFacilitiesDTO
    {
        public string MuseumId { get; set; }
        public bool HasWifi { get; set; }
        public bool IsWheelchairAccessible { get; set; }
        public bool HasAudioGuide { get; set; }
        public bool HasLockers { get; set; }
        public string? WifiPassword { get; set; }
        public string? AudioGuideLanguages { get; set; }
        public string? Notes { get; set; }
    }

    public class UpdateMuseumFacilitiesDTO
    {
        public bool HasWifi { get; set; }
        public bool IsWheelchairAccessible { get; set; }
        public bool HasAudioGuide { get; set; }
        public bool HasLockers { get; set; }
        public string? WifiPassword { get; set; }
        public string? AudioGuideLanguages { get; set; }
        public string? Notes { get; set; }
    }
}
