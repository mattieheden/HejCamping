namespace HejCamping.Domain
{
    public class Review
    {
        public string? OrderNumber { get; set; }
        public string? Name { get; set; }
        public string? ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }
    
    public Review(string orderNumber, string name, string reviewText, DateTime reviewDate)
    {
        OrderNumber = orderNumber;
        Name = name;
        ReviewText = reviewText;
        ReviewDate = reviewDate;
    }
}
}