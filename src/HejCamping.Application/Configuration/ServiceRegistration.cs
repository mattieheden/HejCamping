using Microsoft.Extensions.DependencyInjection;
using HejCamping.Application.Interfaces;
using HejCamping.Application.Services;

namespace HejCamping.Application.Configuration
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IContactService, ContactService>();
        }
    }
}