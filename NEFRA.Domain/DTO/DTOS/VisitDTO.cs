namespace NEEFRA_API.DTO
{
    public class CheckLocationDTO
    {
        public string MuseumId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string VisitType { get; set; } // solo or group
        public string? GroupId { get; set; }  // لو group بس
    }
    public class VisitDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string MuseumId { get; set; }
        public string VisitType { get; set; }
        public DateTime? StartTime { get; set; }
        public bool IsInsideMuseum { get; set; }
       
    }

    public class StartVisitDTO
    {
        public string MuseumId { get; set; }
        //public string Visit_Type { get; set; }
    }
    public class EndVisitDTO
    {
        public string VisitId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string MuseumName { get; set; }
        public string MuseumLocation { get; set; }
        public string MuseumGeneralInfo { get; set; }
        public string MuseumOpenHours { get; set; }
        public decimal TicketEgyptAdultPrice { get; set; }
        public decimal TicketEgyptStudentPrice { get; set; }
        public decimal TicketForienerAdultPrice { get; set; }
        public decimal TicketForienerStudentPrice { get; set; }
       
    }
      
}
