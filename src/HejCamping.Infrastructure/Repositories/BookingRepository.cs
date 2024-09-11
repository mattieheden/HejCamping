using System.Linq;
using HejCamping.Domain.Entities;
using HejCamping.Domain.Repositories;
using HejCamping.Infrastructure.Persistence;

namespace HejCamping.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;
        private readonly int cabinAmount = 5;

        public List<Booking> GetAllBookings()
        {
            List<Booking> bookings = _context.Bookings.ToList();

            return bookings; 
        }
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
            Booking? booking = GetBookingByOrderNr(orderNumber); // Check this with Kim
            if (booking != null)  {
                booking.IsCancelled = true;
                _context.SaveChanges();
            }
        }

        public Dictionary<int, bool> GetCabinAvailability(DateTime dateStart, DateTime dateEnd)
        {
            // Find all bookings that overlap with the given date range that aren't cancelled.
            var bookings = _context.Bookings
                .Where(b => b.DateStart != DateTime.MinValue && b.DateEnd != DateTime.MinValue)
                .Where(b => b.DateStart <= dateEnd && b.DateEnd >= dateStart && !b.IsCancelled)
                .ToList();
            
            // Create a dictionary with all cabins and set them to available.
            Dictionary<int, bool> cabinAvailability = new Dictionary<int, bool>();
            for (int i = 1; i <= cabinAmount; i++)
            {
                cabinAvailability.Add(i, true);
            }

            // Set cabins that are booked in the given date range to unavailable based on cabin number.
            // Console.WriteLine("Test initiation time: " + DateTime.Now);
            // Console.WriteLine("Date start: " + dateStart + " Date end: " + dateEnd + "Bookings: " + bookings.Count);
            foreach (var booking in bookings)
            {
                // Console.WriteLine("Cabin: " + booking.CabinNr + " Dates: " + booking.DateStart + " - " + booking.DateEnd);
                cabinAvailability[booking.CabinNr] = false;
            }

            return cabinAvailability;
        }

        public List<Cabin> GetCabins()
        {
            return new List<Cabin>
            {
                new Cabin { Id = 1, Number = 1, IsVacant = true, PositionX = 13.00, PositionY = 59.00, PricePerNight = 179 }, 
                new Cabin { Id = 2, Number = 2, IsVacant = true, PositionX = 19.00, PositionY = 66.00, PricePerNight = 179 }, 
                new Cabin { Id = 3, Number = 3, IsVacant = true, PositionX = 25.00, PositionY = 70.00, PricePerNight = 179 }, 
                new Cabin { Id = 4, Number = 4, IsVacant = false, PositionX = 32.00, PositionY = 74.00, PricePerNight = 179 }, 
                new Cabin { Id = 5, Number = 5, IsVacant = true, PositionX = 38.50, PositionY = 77.00, PricePerNight = 179 }, 
            };
        }




        public int GetCabinPrice(int cabinId)
        {
            var cabin = GetCabins().FirstOrDefault(c => c.Id == cabinId);
            var price = cabin?.PricePerNight ?? 0;
            return price;
        }
    }
    
}