using HejCamping.Application.DTOs;
using HejCamping.Web.Models;

namespace HejCamping.Application.Interfaces
{
    public interface IBookingService
    {
        BookingDTO GetBookingByOrderNr(string orderNumber);
        void AddBooking(BookingDTO booking);
        void CancelBooking(string orderNumber);
        Dictionary<int, bool> GetCabinAvailability(DateTime dateStart, DateTime dateEnd);
        // JsonResult GetCabinAvailability(DateTime dateStart, DateTime dateEnd);
        List<CabinViewModel> GetCabins();
        Task BookingConfirmationEmail(BookingDTO booking);
        Task CancelBookingConfirmationEmail(string orderNumber);
        int GetCabinPrice(int cabinNr);
    }
}