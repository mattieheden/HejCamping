namespace HejCamping.ApplicationServices
{
    public class ReviewDTO
    {
        public string OrderNumber { get; set; }
        public string? Name { get; set; }
        public string? ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}