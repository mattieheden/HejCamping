using HejCamping.Domain;
using HejCamping.Models;

namespace HejCamping.ApplicationServices
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmailService _emailService;

        public BookingService(IBookingRepository bookingRepository, IEmailService emailService)
        {
            _bookingRepository = bookingRepository;
            _emailService = emailService;
        }

        public List<BookingDTO> GetAllBookings()
        {
            var bookings = _bookingRepository.GetAllBookings();

            return bookings.Select(booking => new BookingDTO{
                OrderNumber = booking.OrderNumber,
                IsCancelled = booking.IsCancelled,
                OrderDate = booking.OrderDate,
                Email = booking.Email,
                Name = booking.Name,
                DateStart = booking.DateStart,
                DateEnd = booking.DateEnd,
                CabinNr = booking.CabinNr,
                TotalPrice = booking.TotalPrice
            }).ToList();
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
        }

        public void AddBooking(BookingDTO booking)
        {
            _bookingRepository.AddBooking(new Booking(booking.OrderNumber, booking.IsCancelled, booking.OrderDate, booking.Email, booking.Name, booking.DateStart, booking.DateEnd, booking.CabinNr, booking.TotalPrice));
        }

        public async void CancelBooking(string orderNumber)
        {
            _bookingRepository.CancelBooking(orderNumber);
            await CancelBookingConfirmationEmail(orderNumber);
        }

        public Dictionary<int, bool> GetCabinAvailability(DateTime dateStart, DateTime dateEnd)
        {
            return _bookingRepository.GetCabinAvailability(dateStart, dateEnd);
        }

        public List<Cabin> GetCabins()
        {
            return _bookingRepository.GetCabins();
        }

        public async Task BookingConfirmationEmail(BookingDTO booking)
        {
            string subject = $"Booking confirmation for {booking.OrderNumber}";
            string htmlBody = $"<h1>Dear {booking.Name},</h1>" + 
                            "<p>Thank you for booking a cabin at Hej Camping!</p><br />" + 
                            "<p>Your booking details:</p>" + 
                            $"<p>Order number: {booking.OrderNumber}</p>" + 
                            $"<p>Cabin number: {booking.CabinNr}</p>" + 
                            $"<p>Check-in: {booking.DateStart.ToString().Substring(0,10)} at 15:00</p>" + 
                            $"<p>Check-out: {booking.DateEnd.ToString().Substring(0,10)} at 12:00</p>" + 
                            $"<p>Total price: {booking.TotalPrice}</p><br />" + 
                            "<p>We look forward to seeing you!</p><br />" + 
                            "<p>Best regards,<br />Hej Camping</p>";
            
            await _emailService.SendEmailAsync(booking.Email, subject, htmlBody);
        }

        public async Task CancelBookingConfirmationEmail(string orderNumber)
        {
            var booking = _bookingRepository.GetBookingByOrderNr(orderNumber);
            string subject = $"Booking cancellation for {booking.OrderNumber}";
            string htmlBody = $"<h1>Dear {booking.Name},</h1>" + 
                            "<p>We are sorry that you had to cancel your booking at Hej Camping and hope that it will work out better next time.</p><br />" +
                            "<p>Your booking details:</p>" +
                            $"<p>Order number: {booking.OrderNumber}</p>" +
                            $"<p>Cabin number: {booking.CabinNr}</p>" +
                            $"<p>Order dates: {booking.DateStart.ToString().Substring(0,10)} - {booking.DateEnd.ToString().Substring(0,10)}</p>" +
                            $"<p>Total price: {booking.TotalPrice}</p><br />" +
                            "<p>We hope to see you again soon!</p><br />" +
                            "<p>Best regards,<br />Hej Camping</p>";
            
            // Probably should do some error handling, it should never be null here if a booking went through but still..
            if (booking.Email == null) return;

            await _emailService.SendEmailAsync(booking.Email, subject, htmlBody);
        }

        public int GetCabinPrice(int cabinNr)
        {
            return _bookingRepository.GetCabinPrice(cabinNr);
        }

    }
}
