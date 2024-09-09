using HejCamping.Application.Interfaces;
using HejCamping.Domain.Entities;
using HejCamping.Domain.Services;


namespace HejCamping.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IEmailService _emailService;

        public ContactService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendContactEmailAsync(string fromEmail, string orderNumber, string subject, string body)
        {
            if (orderNumber == null) orderNumber = "N/A";
            if (subject == null) subject = "Website Inquiry";

            var message = $"<p>From: {fromEmail}</p>" +
                          $"<p>Order number: {orderNumber}</p>" +
                          $"<p>{body}</p>";
                          
            await _emailService.SendEmailAsync("Owner", subject, message);
        }
    }
}