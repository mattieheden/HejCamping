namespace HejCamping.Infrastructure.Options
{
    public class AzureEmailSettings
    {
        public required string ConnectionString { get; set; }
        public required string SenderEmail { get; set; }
        public required string OwnerEmail { get; set; }
    }
}