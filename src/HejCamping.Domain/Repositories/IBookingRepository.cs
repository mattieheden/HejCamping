using HejCamping.Domain.Entities;

namespace HejCamping.Domain.Repositories
{
    public interface IBookingRepository
    {
        List<Booking> GetAllBookings();
        Booking GetBookingByOrderNr(string orderNumber);
        void AddBooking(Booking booking);
        void CancelBooking(string orderNumber);
        Dictionary<int, bool> GetCabinAvailability(DateTime dateStart, DateTime dateEnd);
        List<Cabin> GetCabins();
        int GetCabinPrice(int cabinNr);

    }
}

