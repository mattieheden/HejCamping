namespace HejCamping.Models
{
    public class BookingModel
    {
        public int BookingId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int CabinId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}