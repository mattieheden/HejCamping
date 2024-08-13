namespace HejCamping.Interfaces;

public interface IBookingRepository
{
    IEnumerable<Booking> GetAllBookings();
    Booking GetBookingByDate(DateTime date);
    void AddBooking(Booking booking);
    void UpdateBooking(Booking booking);
    void DeleteBooking(string orderNumber);
}