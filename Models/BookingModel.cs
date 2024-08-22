namespace HejCamping.Models
{
    public class BookingModel
    {
        public int OrderNumber { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime OrderDate { get; set; }
        public int CabinId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public float PricePerNight { get; set; }  
        public int NumberOfNights { get; set; }     
        public float TotalPrice { get; set; } 
    }
}