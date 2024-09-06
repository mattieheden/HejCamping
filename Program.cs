using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

using HejCamping.ApplicationServices;
using HejCamping.Domain;
using HejCamping.Infrastructure;
using HejCamping.Infrastructure.Options;

using HejCamping.Application.Configuration;
using HejCamping.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}


// Load environment variables
var configuration = builder.Configuration;
configuration.AddEnvironmentVariables(); // Add environment variables to configuration

// Use environment variables for sensitive settings
var emailSettings = new AzureEmailSettings
{
    ConnectionString = Environment.GetEnvironmentVariable("AZURE_EMAIL_CONNECTION_STRING"),
    SenderEmail = Environment.GetEnvironmentVariable("AZURE_EMAIL_SENDER"),
    OwnerEmail = Environment.GetEnvironmentVariable("AZURE_EMAIL_OWNER")
};

var dbConnectionString = builder.Configuration.GetValue<string>("DATABASE_CONNECTION_STRING");

if (string.IsNullOrEmpty(dbConnectionString))
{
    throw new InvalidOperationException("The database connection string has not been set.");
}

// Bind AzureEmailService settings
builder.Services.Configure<AzureEmailSettings>(options =>
{
    options.ConnectionString = emailSettings.ConnectionString;
    options.SenderEmail = emailSettings.SenderEmail;
    options.OwnerEmail = emailSettings.OwnerEmail;
});

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        dbConnectionString,
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Maximum number of retry attempts
            maxRetryDelay: TimeSpan.FromSeconds(30), // Maximum delay between retries
            errorNumbersToAdd: null // List of additional SQL error numbers to consider transient
        )));
builder.Services.AddScoped<IEmailService, AzureEmailService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

var app = builder.Build();

// Automatically apply pending migrations on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
    
app.Run();
