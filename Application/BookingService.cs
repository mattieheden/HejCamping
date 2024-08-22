using HejCamping.Domain;

namespace HejCamping.ApplicationServices
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public BookingDTO GetBookingByOrderNr(string orderNumber)
        {
            var booking = _bookingRepository.GetBookingByOrderNr(orderNumber);
            return new BookingDTO{
                OrderNumber = booking.OrderNumber,
                IsCancelled = booking.IsCancelled,
                OrderDate = booking.OrderDate,
                Email = booking.Email,
                Name = booking.Name,
                DateStart = booking.DateStart,
                DateEnd = booking.DateEnd,
                CabinNr = booking.CabinNr,
                TotalPrice = booking.TotalPrice
            };
            // return new BookingDTO();
        }

        public void AddBooking(BookingDTO booking)
        {
            _bookingRepository.AddBooking(new Booking(booking.OrderNumber, booking.IsCancelled, booking.OrderDate, booking.Email, booking.Name, booking.DateStart, booking.DateEnd, booking.CabinNr, booking.TotalPrice));
        }

        public void CancelBooking(string orderNumber)
        {
            _bookingRepository.CancelBooking(orderNumber);
        }

        public Dictionary<int, bool> GetCabinAvailability(DateTime dateStart, DateTime dateEnd)
        {
            return _bookingRepository.GetCabinAvailability(dateStart, dateEnd);
        }
    }
}