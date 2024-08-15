namespace HejCamping.Models
{
    public class BookingModel
    {
        public List<Campsite> Campsites { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int SelectedCampsiteId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}