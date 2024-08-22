using System.Linq;
using HejCamping.Domain;

namespace HejCamping.Infrastructure
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;
        private readonly int cabinAmount = 5;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }
        public Booking GetBookingByOrderNr(string orderNumber)
        {
                Booking booking = _context.Bookings.FirstOrDefault(b => b.OrderNumber == orderNumber);
                return booking;
        }

        public void AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public void CancelBooking(string orderNumber)
        {
            Booking booking = GetBookingByOrderNr(orderNumber);
            if (booking != null)  {
                booking.IsCancelled = true;
                _context.SaveChanges();
            }
        }

        public Dictionary<int, bool> GetCabinAvailability(DateTime dateStart, DateTime dateEnd)
        {
            // Find all bookings that overlap with the given date range that aren't cancelled.
            var bookings = _context.Bookings.Where(b => b.DateStart < dateEnd && b.DateEnd > dateStart && !b.IsCancelled).ToList();

            // Create a dictionary with all cabins and set them to available.
            Dictionary<int, bool> cabinAvailability = new Dictionary<int, bool>();
            for (int i = 1; i <= cabinAmount; i++)
            {
                cabinAvailability.Add(i, true);
            }

            // Set cabins that are booked in the given date range to unavailable based on cabin number.
            foreach (var booking in bookings)
            {
                cabinAvailability[booking.CabinNr] = false;
            }

            return cabinAvailability;
        }
            
    }
}