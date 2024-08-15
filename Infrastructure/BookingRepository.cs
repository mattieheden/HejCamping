using HejCamping.Domain;

namespace HejCamping.Infrastructure
{
    public class BookingRepository : IBookingRepository
    {
        public Booking GetBookingByOrderNr(string orderNumber)
        {
            
            return new Booking("123", false, System.DateTime.Now, "test@email.com",
                "John Doe", System.DateTime.Now.AddDays(1), System.DateTime.Now.AddDays(4), 1, 50.0f);
        }
    }
}