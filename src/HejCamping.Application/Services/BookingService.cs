using HejCamping.Application.DTOs;
using HejCamping.Application.Interfaces;
using HejCamping.Domain.Entities;
using HejCamping.Domain.Repositories;
using HejCamping.Domain.Services;


namespace HejCamping.Application.Services
{

    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IViewRenderer _viewRenderer;
        private readonly IEmailService _emailService;

        public BookingService(IBookingRepository bookingRepository, IViewRenderer viewRenderer, IEmailService emailService)
        {
            _bookingRepository = bookingRepository;
            _viewRenderer = viewRenderer;
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
            return new BookingDTO
            {
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

        public Dictionary<int, bool> GetCabinAvailability(System.DateTime dateStart, System.DateTime dateEnd)
        {
            return _bookingRepository.GetCabinAvailability(dateStart, dateEnd);
        }

        public List<Cabin> GetCabins()
        {
            List<Cabin> cabins = _bookingRepository.GetCabins();
            return cabins;
        }

        public async Task BookingConfirmationEmail(BookingDTO booking)
        {
            if (booking.Email == null) return; //Same as in CancelBooking. Do same error handling in separate function?

            string subject = $"Booking confirmation for {booking.OrderNumber}";
            string htmlBody = await _viewRenderer.RenderViewToStringAsync("Emails/BookingConfirmation", booking);

            await _emailService.SendEmailAsync(booking.Email, subject, htmlBody);
        }

        public async Task CancelBookingConfirmationEmail(string orderNumber)
        {
            Booking booking = _bookingRepository.GetBookingByOrderNr(orderNumber);
            if (booking == null) 
            {
                Console.WriteLine("Booking not found");
                return;
            }
            if (booking.Email == null) return; //Should probably add some error handling here

            string subject = $"Booking cancellation for {booking.OrderNumber}";
            string htmlBody = await _viewRenderer.RenderViewToStringAsync("Emails/CancelBookingConfirmation", booking);

            await _emailService.SendEmailAsync(booking.Email, subject, htmlBody);
        }

        public int GetCabinPrice(int cabinNr)
        {
            return _bookingRepository.GetCabinPrice(cabinNr);
        }
    }
}