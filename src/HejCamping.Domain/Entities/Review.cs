using System.ComponentModel.DataAnnotations;

namespace HejCamping.Domain.Entities
{
    public class Review
    {
        public string? OrderNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string ReviewText { get; set; }
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