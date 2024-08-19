using System.Linq;
using HejCamping.Domain;

namespace HejCamping.Infrastructure
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }
        public Booking GetBookingByOrderNr(string orderNumber)
        {
            try
            {
                Booking booking = _context.Bookings.FirstOrDefault(b => b.OrderNumber == orderNumber);
                return booking;
            }
            catch
            {
                return null;
            }
        }

        public void AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }
    }
}