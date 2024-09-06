using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using HejCamping.Domain.Repositories;
using HejCamping.Domain.Services;

using HejCamping.Infrastructure.Options;
using HejCamping.Infrastructure.Persistence;
using HejCamping.Infrastructure.Repositories;
using HejCamping.Infrastructure.Services;

namespace HejCamping.Infrastructure.Configuration
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind AzureEmailSettings to the configuration section "AzureEmailSettings"
            services.Configure<AzureEmailSettings>(configuration.GetSection("AzureEmailSettings"));

            // Bind DatabaseOptions to the configuration section "ConnectionStrings"
            services.Configure<DatabaseOptions>(configuration.GetSection("ConnectionStrings"));
            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                // Get the connection string from the DatabaseOptions
                var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                options.UseSqlite(databaseOptions.DefaultConnection);
            });




            // Add services to the container
            services.AddScoped<IEmailService, AzureEmailService>();
            services.AddScoped<IBookingRepository, BookingRepository>();
        }
    }
}