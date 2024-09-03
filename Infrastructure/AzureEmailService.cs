using Azure.Communication.Email;

using HejCamping.Domain;
using HejCamping.Infrastructure.Options;
using Microsoft.Extensions.Options;


namespace HejCamping.Infrastructure
{
    public class AzureEmailService : IEmailService
    {
        private readonly AzureEmailSettings _azureEmailSettings;

        public AzureEmailService(IOptions<AzureEmailSettings> azureEmailSettings)
        {
            _azureEmailSettings = azureEmailSettings.Value;
        }
        public async Task SendEmailAsync(string recipientAddress, string subject, string body)
        {
            if (recipientAddress == "Owner")
            {
                recipientAddress = _azureEmailSettings.OwnerEmail;
                Console.WriteLine("Recipient address changed to Owner");
            }
            EmailClient _emailClient = new EmailClient(_azureEmailSettings.ConnectionString);
            EmailSendOperation emailSendOperation = _emailClient.Send(
                Azure.WaitUntil.Started,
                senderAddress: _azureEmailSettings.SenderEmail,
                recipientAddress,
                subject: subject,
                htmlContent: body);
            await emailSendOperation.WaitForCompletionAsync();
        }
    }
}