using Microsoft.EntityFrameworkCore;

using HejCamping.Domain.Repositories;
using HejCamping.Domain.Services;

using HejCamping.Infrastructure.Options;
using HejCamping.Infrastructure.Persistence;
using HejCamping.Infrastructure.Repositories;
using HejCamping.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.ViewEngines;


namespace HejCamping.Infrastructure.Configuration
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Fetch Settings from environment variables
            var emailSettings = new AzureEmailSettings
            {
                ConnectionString = Environment.GetEnvironmentVariable("AZURE_EMAIL_CONNECTION_STRING"),
                SenderEmail = Environment.GetEnvironmentVariable("AZURE_EMAIL_SENDER"),
                OwnerEmail = Environment.GetEnvironmentVariable("AZURE_EMAIL_OWNER")
            };

            var dbConnectionString = configuration.GetValue<string>("DATABASE_CONNECTION_STRING");

            if (string.IsNullOrEmpty(dbConnectionString))
            {
                throw new InvalidOperationException("The database connection string has not been set.");
            }

            services.Configure<AzureEmailSettings>(options =>
                {
                    options.ConnectionString = emailSettings.ConnectionString;
                    options.SenderEmail = emailSettings.SenderEmail;
                    options.OwnerEmail = emailSettings.OwnerEmail;
                });

            // Bind DatabaseOptions to the configuration section "ConnectionStrings"
            services.Configure<DatabaseOptions>(configuration.GetSection("ConnectionStrings"));
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    dbConnectionString,
                    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 5, // Maximum number of retry attempts
                        maxRetryDelay: TimeSpan.FromSeconds(30), // Maximum delay between retries
                        errorNumbersToAdd: null // List of additional SQL error numbers to consider transient
            )));




            // Add services to the container
            services.AddScoped<IViewRenderer, ViewRendererService>();
            services.AddScoped<IEmailService, AzureEmailService>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
        }
    }
}