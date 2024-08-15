using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HejCamping.Models;

namespace HejCamping.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public List<Campsite> GetCampsites()
        {
            return new List<Campsite>
            {
                new Campsite { Id = 1, Number = 1, IsVacant = true, PositionX = 100, PositionY = 230 },
                new Campsite { Id = 2, Number = 2, IsVacant = true, PositionX = 150, PositionY = 270 },
                new Campsite { Id = 3, Number = 3, IsVacant = true, PositionX = 200, PositionY = 290 },
                new Campsite { Id = 4, Number = 4, IsVacant = false, PositionX = 250, PositionY = 310 },
                new Campsite { Id = 5, Number = 5, IsVacant = true, PositionX = 300, PositionY = 320 },
            };
        }
        /*public IActionResult Map()
        {
            var campsites = GetCampsites();
            return View(campsites);
        }*/

        public IActionResult Booking()
        {
            var model = new BookingModel
            {
                Campsites = GetCampsites(), // Fetches the campsite data using the reusable method
                FromDate = DateTime.Now,
                ToDate = DateTime.Now.AddDays(1)
            };

            return View(model);
        }
        // Index action
        public IActionResult Index()
        {
            return View();
        }

        // Privacy action
        public IActionResult Privacy()
        {
            return View();
        }

        // DatePicker action - GET
        [HttpGet]
        public IActionResult DatePicker()
        {
            return View();
        }

        // DatePicker action - POST
        [HttpPost]
        public IActionResult DatePicker(DatePickerModel model)
        {
            // Process the selected date
            return View(model);
        }

        // Error action
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
