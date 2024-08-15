namespace HejCamping.Domain
{
    public interface IBookingRepository
    {
        Booking GetBookingByOrderNr(string orderNumber);
    }
}

