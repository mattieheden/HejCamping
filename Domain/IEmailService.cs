namespace HejCamping.Domain
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}