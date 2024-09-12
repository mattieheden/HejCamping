using HejCamping.Application.DTOs;
using HejCamping.Domain.Entities;

namespace HejCamping.Application.Interfaces
{
    public interface IBookingService
    {
        List<BookingDTO> GetAllBookings();
        BookingDTO GetBookingByOrderNr(string orderNumber);
        void AddBooking(BookingDTO booking);
        void CancelBooking(string orderNumber);
        void RestoreBooking(string orderNumber);
        Dictionary<int, bool> GetCabinAvailability(DateTime dateStart, DateTime dateEnd);
        // JsonResult GetCabinAvailability(DateTime dateStart, DateTime dateEnd);
        List<Cabin> GetCabins();
        Task BookingConfirmationEmail(BookingDTO booking);
        Task CancelBookingConfirmationEmail(string orderNumber);
        int GetCabinPrice(int cabinNr);
    }
}