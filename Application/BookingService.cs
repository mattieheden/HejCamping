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
            var booking = _bookingRepository.GetBookingByOrderNr("123");
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
        }
    }
}