namespace NEEFRA_API.DTO
{
    public class MuseumDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string GeneralInfo { get; set; }
        public string Open_Hours { get; set; }
        public decimal TicketEgyptAdultPrice { get; set; }
        public decimal TicketEgyptStudentPrice { get; set; }
        public decimal TicketForienerAdultPrice { get; set; }
        public decimal TicketForienerStudentPrice { get; set; }
        public string GovernorateName { get; set; }
    }
}
