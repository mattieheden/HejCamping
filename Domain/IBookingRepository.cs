namespace HejCamping.Domain
{
    public interface IBookingRepository
    {
        public Booking GetBookingByOrderNr(string orderNumber);
        public void AddBooking(Booking booking);
        public void CancelBooking(string orderNumber);
    }
}

