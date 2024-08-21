namespace HejCamping.ApplicationServices
{
    public class ReviewDTO
    {
        public int OrderNumber { get; set; }
        public string? Name { get; set; }
        public string? ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}