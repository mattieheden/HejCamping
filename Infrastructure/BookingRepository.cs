using System.Linq;
using HejCamping.Domain;
using HejCamping.Models;

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
            return new Dictionary<int, bool>
            {
                {1, true},
                {2, false},
                {3, true},
                {4, false},
                {5, true}
            };
        }
        /*public Dictionary<int, bool> GetCabinAvailability(DateTime dateStart, DateTime dateEnd)
        {
            var allCabins = GetCabins();
            var bookedCabins = _context.Bookings
                .Where(b => dateEnd >= b.FromDate && dateStart <= b.ToDate)
                .Select(b => b.CabinId)
                .Distinct()
                .ToList();

            var availabilityDict = allCabins.ToDictionary(
                c => c.Id,
                c => !bookedCabins.Contains(c.Id) 
            );

            return availabilityDict;
        }*/
        public List<Cabin> GetCabins()
        {
            return new List<Cabin>
            {
                new Cabin { Id = 1, Number = 1, IsVacant = true, PositionX = 100, PositionY = 230 },
                new Cabin { Id = 2, Number = 2, IsVacant = true, PositionX = 150, PositionY = 270 },
                new Cabin { Id = 3, Number = 3, IsVacant = true, PositionX = 200, PositionY = 290 },
                new Cabin { Id = 4, Number = 4, IsVacant = true, PositionX = 250, PositionY = 310 },
                new Cabin { Id = 5, Number = 5, IsVacant = true, PositionX = 300, PositionY = 320 },
            };
        }
    }
}