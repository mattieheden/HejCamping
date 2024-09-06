namespace HejCamping.Domain.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipientAddress, string subject, string body);
    }
}