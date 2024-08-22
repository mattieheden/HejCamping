namespace HejCamping.ApplicationServices
{
    public interface IBookingService
    {
        BookingDTO GetBookingByOrderNr(string orderNumber);
        void AddBooking(BookingDTO booking);
        void CancelBooking(string orderNumber);
        Dictionary<int, bool> GetCabinAvailability(DateTime dateStart, DateTime dateEnd);
    }
}
