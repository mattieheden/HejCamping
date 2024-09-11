namespace HejCamping.Web.Models
{
    public class CabinViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool IsVacant { get; set; }
        public double PositionX { get; set; } 
        public double PositionY { get; set; } 
        public int PricePerNight { get; set; }
    }
}
