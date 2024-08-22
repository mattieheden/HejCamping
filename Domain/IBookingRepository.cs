using HejCamping.Models;
namespace HejCamping.Domain
{
    public interface IBookingRepository
    {
        Booking GetBookingByOrderNr(string orderNumber);
        void AddBooking(Booking booking);
        void CancelBooking(string orderNumber);
        Dictionary<int, bool> GetCabinAvailability(DateTime dateStart, DateTime dateEnd);
        List<Cabin> GetCabins();

    }
}

