using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HejCamping.Models;
using HejCamping.ApplicationServices;
using HejCamping.Context;

namespace HejCamping.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Map()
        {
            var campsites = new List<Campsite>
            {
                new Campsite { Id = 1, Number = 1, IsVacant = true, PositionX = 100, PositionY = 230 },
                new Campsite { Id = 2, Number = 2, IsVacant = false, PositionX = 150, PositionY = 270 },
                new Campsite { Id = 3, Number = 3, IsVacant = true, PositionX = 200, PositionY = 290 },
                new Campsite { Id = 4, Number = 4, IsVacant = false, PositionX = 250, PositionY = 310 },
                new Campsite { Id = 5, Number = 5, IsVacant = true, PositionX = 300, PositionY = 320 },
            };

            return View(campsites);
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    //CRUD tester /Kim
    // public IActionResult CreateBooking(String email, String name, DateTime dateStart, DateTime dateEnd, int cabinNr)
    // {
    //     var booking = new BookingDTO{OrderNumber = "123", IsCancelled = false, OrderDate = DateTime.Now, Email = email, Name = name, DateStart = dateStart, DateEnd = dateEnd, CabinNr = cabinNr};
    //     _context.Bookings.Add(booking);
    //     _context.SaveChanges();
    //     return RedirectToAction();//Redirect to Order confirmation page
    // }
}
