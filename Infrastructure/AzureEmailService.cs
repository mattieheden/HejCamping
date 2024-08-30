using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure;
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


// This code retrieves your connection string from an environment variable.
// var emailClient = new EmailClient(connectionString);


// EmailSendOperation emailSendOperation = emailClient.Send(
//     WaitUntil.Completed,
//     senderAddress: "DoNotReply@e92803ae-1188-4361-8061-bb2c34dbeb00.azurecomm.net",
//     recipientAddress: "reejfaviwcarisulas@poplk.com",
//     subject: "Test Email",
//     htmlContent: "<html><h1>Hello world via email.</h1l></html>",
//     plainTextContent: "Hello world via email.");