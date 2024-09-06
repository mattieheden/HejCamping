using System.Threading.Tasks;

using HejCamping.Application.DTOs;
using HejCamping.Application.Interfaces;
using HejCamping.Domain.Entities;
using HejCamping.Domain.Repositories;
using HejCamping.Domain.Services;
using HejCamping.Web.Models;


namespace HejCamping.Application.Services
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

        public List<CabinViewModel> GetCabins()
        {
            //Temporary conversation from Cabin to CabinViewModel, Might keep Cabin as an entity in the domain layer
            List<Cabin> cabins = _bookingRepository.GetCabins();
            List<CabinViewModel> cabinmodel = new List<CabinViewModel>();
            foreach (var cabin in cabins)
            {
                cabinmodel.Add(new CabinViewModel
                {
                    Id = cabin.Id,
                    Number = cabin.Number,
                    IsVacant = cabin.IsVacant,
                    PositionX = cabin.PositionX,
                    PositionY = cabin.PositionY,
                    PricePerNight = cabin.PricePerNight
                });
            }

            return cabinmodel;
        }

        public async Task BookingConfirmationEmail(BookingDTO booking)
        {
            string subject = $"Booking confirmation for {booking.OrderNumber}";
            string htmlBody = $"<h1>Dear {booking.Name},</h1>" + 
                            "<p>Thank you for booking a cabin at Hej Camping!</p><br />" + 
                            "<p>Your booking details:</p>" + 
                            $"<p>Order number: {booking.OrderNumber}<br />" + 
                            $"Cabin number: {booking.CabinNr}<br />" + 
                            $"Check-in: {booking.DateStart.ToString()[..10]} at 15:00<br />" + 
                            $"Check-out: {booking.DateEnd.ToString()[..10]} at 12:00<br />" + 
                            $"Total price: {booking.TotalPrice}</p><br />" + 
                            "<p>We look forward to seeing you!</p><br />" + 
                            "<p>Best regards,<br />Hej Camping</p>";
            //Same as in CancelBooking. Do same error handling in separate function?
            if (booking.Email == null) return;

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
            string subject = $"Booking cancellation for {booking.OrderNumber}";
            string htmlBody = $"<h1>Dear {booking.Name},</h1>" + 
                            "<p>We are sorry that you had to cancel your booking at Hej Camping and hope that it will work out better next time.</p><br />" +
                            "<p>Your booking details:</p>" +
                            $"<p>Order number: {booking.OrderNumber}</p>" +
                            $"<p>Cabin number: {booking.CabinNr}</p>" +
                            $"<p>Order dates: {booking.DateStart.ToString()[..10]} - {booking.DateEnd.ToString()[..10]}</p>" +
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