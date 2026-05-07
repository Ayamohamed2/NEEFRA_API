namespace NEEFRA.Core.DTO
{
    public class PathPointDTO
    {
        public int Order { get; set; }
        public string Type { get; set; } = "piece"; // "start" or "piece"
        public string Name { get; set; } = string.Empty;
        public int? Floor { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int? Score { get; set; }
        public bool Valid { get; set; }
        public double DistanceFromPrevMeters { get; set; }
    }

    public class RouteResultDTO
    {
        public List<PathPointDTO> Path { get; set; } = new();
        public double TotalDistanceMeters { get; set; }
    }
}
