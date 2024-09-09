using HejCamping.Application.Interfaces;
using HejCamping.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HejCamping.Application.Configuration
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IReviewService, ReviewService>();
        }
    }
}