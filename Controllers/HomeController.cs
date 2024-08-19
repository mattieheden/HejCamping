using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HejCamping.Models;
using HejCamping.ApplicationServices;

namespace HejCamping.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookingService _bookingService;

        public HomeController(ILogger<HomeController> logger, IBookingService bookingService)
        {
            _logger = logger;
            _bookingService = bookingService;
        }

        public List<Cabin> GetCabins()
        {
            //replace mock response with database query? 
            return new List<Cabin>
            {
                new Cabin { Id = 1, Number = 1, IsVacant = true, PositionX = 100, PositionY = 230 },
                new Cabin { Id = 2, Number = 2, IsVacant = true, PositionX = 150, PositionY = 270 },
                new Cabin { Id = 3, Number = 3, IsVacant = true, PositionX = 200, PositionY = 290 },
                new Cabin { Id = 4, Number = 4, IsVacant = false, PositionX = 250, PositionY = 310 },
                new Cabin { Id = 5, Number = 5, IsVacant = true, PositionX = 300, PositionY = 320 },
            };
        }

        public IActionResult Booking()
        {
            var model = new BookingModel
            {
                FromDate = DateTime.Now,
                ToDate = DateTime.Now.AddDays(1)
            };

            var cabins = GetCabins();
            ViewBag.Cabins = cabins;
            return View(model);
        }

        [HttpPost]
        public IActionResult SubmitBooking(BookingModel model)
        {
            if (ModelState.IsValid)
            {
                // Mock booking ID, real one should be generated by the database
                model.BookingId = new Random().Next(1000, 9999);
                model.OrderDate = DateTime.Now;

                // Send booking to database (Currently fake data)
                _bookingService.AddBooking(new BookingDTO
                {
                    OrderNumber = model.BookingId.ToString(),
                    IsCancelled = false,
                    OrderDate = model.OrderDate,
                    Email = model.Email,
                    Name = model.Name,
                    DateStart = model.FromDate,
                    DateEnd = model.ToDate,
                    CabinNr = model.CabinId,
                    TotalPrice = 100
                });
                // Redirect to a confirmation view
                return View("BookedCabin", model);
            }
            else
            {
                // If the model state is invalid, return to the booking page with validation messages
                return View("Booking", model);
            }
        }

        //[HttpPost]
        public JsonResult DelayedRedirect()
        {
        // Your action logic here

        // Return a JSON response
        return Json(new { success = true, message = "Action triggered successfully!" });
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
