namespace HejCamping.ApplicationServices
{
    public interface IBookingService
    {
        BookingDTO GetBookingByOrderNr(string orderNumber);
    }
}
