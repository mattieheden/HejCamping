using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

using HejCamping.ApplicationServices;
using HejCamping.Domain;
using HejCamping.Infrastructure;
using HejCamping.Infrastructure.Options;

using HejCamping.Application.Configuration;
<<<<<<< HEAD
using HejCamping.Domain.Interfaces;
using HejCamping.Infrastructure.Repositories;
=======
using HejCamping.Infrastructure.Configuration;
>>>>>>> 62bd50e (Moved basically everything except datepicker, Reviews and Authentication into clean architecture.)

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
<<<<<<< HEAD


=======
    
>>>>>>> 62bd50e (Moved basically everything except datepicker, Reviews and Authentication into clean architecture.)
app.Run();
