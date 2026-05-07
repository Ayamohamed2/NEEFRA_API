namespace NEEFRA_API.DTO
{
    public class GovernoratePhotoDTO
    {
        public string Id { get; set; }
        public string GovernorateId { get; set; }
        public string PhotoUrl { get; set; }
        public string? Caption { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime UploadedAt { get; set; }
    }

    public class CreateGovernoratePhotoDTO
    {
        public string GovernorateId { get; set; }
        public string PhotoUrl { get; set; }
        public string? Caption { get; set; }
        public bool IsPrimary { get; set; } = false;
    }
}
