namespace HejCamping.Domain.Entities
{
    public class Cabin
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool IsVacant { get; set; }
        public int PositionX { get; set; } 
        public int PositionY { get; set; } 
        public int PricePerNight { get; set; }
    }
}
