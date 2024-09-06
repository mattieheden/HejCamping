using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HejCamping.Domain.Entities
{
    public class Booking
    {
        [Key]
        [Column(TypeName = "nvarchar(100)")]
        public string OrderNumber { get; set; }
        
        public bool IsCancelled { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int CabinNr { get; set; }
        public float TotalPrice { get; set; }

        public Booking(string orderNumber, bool isCancelled, DateTime orderDate, string email, string name, DateTime dateStart, DateTime dateEnd, int cabinNr, float totalPrice)
        {
            OrderNumber = orderNumber;
            IsCancelled = isCancelled;
            OrderDate = orderDate;
            Email = email;
            Name = name;
            DateStart = dateStart;
            DateEnd = dateEnd;
            CabinNr = cabinNr;
            TotalPrice = totalPrice;
        }

    }
}

