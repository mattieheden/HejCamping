using HejCamping.Domain;
using HejCamping.Domain.Entities;
using HejCamping.Domain.Interfaces;

namespace HejCamping.ApplicationServices
{
    public interface IBookingService
    {
        BookingDTO GetBookingByOrderNr(string orderNumber);
        void AddBooking(BookingDTO booking);
        void CancelBooking(string orderNumber);
        Dictionary<int, bool> GetCabinAvailability(DateTime dateStart, DateTime dateEnd);
        // JsonResult GetCabinAvailability(DateTime dateStart, DateTime dateEnd);
        List<Cabin> GetCabins();
        Task BookingConfirmationEmail(BookingDTO booking);
        Task CancelBookingConfirmationEmail(string orderNumber);
        int GetCabinPrice(int cabinNr);
    }
}
