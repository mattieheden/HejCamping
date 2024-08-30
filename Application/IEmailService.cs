using HejCamping.Domain;

namespace HejCamping.ApplicationServices
{
    public interface IEmailService
    {
        bool SendBookingEmail(BookingDTO booking);
        bool SendCancellationEmail(BookingDTO booking);
        bool SendContactEmail(ContactDTO contact);
    }
}