using Microsoft.AspNetCore.Mvc;

using HejCamping.Application.DTOs;
using HejCamping.Application.Interfaces;
using HejCamping.Web.Models;
using HejCamping.Application.Services;
using Microsoft.VisualBasic;

namespace HejCamping.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IReviewService _reviewService;

        public BookingController(IBookingService bookingService, IReviewService reviewService)
        {
            _bookingService = bookingService;
            _reviewService = reviewService;
        }

        public IActionResult Index()
        {
            var cabins = _bookingService.GetCabins();
            ViewBag.Cabins = cabins;
            var cabinAvailability = _bookingService.GetCabinAvailability(DateTime.Today, DateTime.Today.AddDays(1));
            foreach (var cabin in cabins)
            {
                cabin.IsVacant = cabinAvailability[cabin.Id];
            }
            ViewBag.CabinAvailability = cabinAvailability;
            return View();
        }

        [HttpGet]
        public IActionResult GetAvailableCabins(string fromDate, string toDate)
        {
            DateTime parsedFromDate;
            DateTime parsedToDate;

            if (!DateTime.TryParse(fromDate, out parsedFromDate) || !DateTime.TryParse(toDate, out parsedToDate))
            {
                return BadRequest("Invalid date format");
            }

            var cabinAvailability = _bookingService.GetCabinAvailability(parsedFromDate, parsedToDate);
            return Json(cabinAvailability);
        }

        [HttpPost]
        public IActionResult SubmitBooking(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.PricePerNight = _bookingService.GetCabinPrice(model.CabinId);
                model.NumberOfNights = (int)(model.ToDate - model.FromDate).TotalDays;
                model.TotalPrice = model.PricePerNight * model.NumberOfNights;
                model.OrderDate = DateTime.UtcNow;
                // Mock booking ID, real one should be generated by the database
                string timestampPart = DateTime.UtcNow.ToString("mmssfff"); 
                string randomLetters = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 7)
                                                .Select(s => s[new Random().Next(s.Length)]).ToArray());
                model.OrderNumber = $"{randomLetters}{timestampPart}";

                // Set up BookingDTO object
                BookingDTO booking = new BookingDTO
                {
                    OrderNumber = model.OrderNumber,
                    IsCancelled = false,
                    OrderDate = model.OrderDate,
                    Email = model.Email,
                    Name = model.Name,
                    DateStart = model.FromDate,
                    DateEnd = model.ToDate,
                    CabinNr = model.CabinId,
                    TotalPrice = model.TotalPrice,
                };

                // Send booking to database
                _bookingService.AddBooking(booking);

                // Send booking confirmation email
                _bookingService.BookingConfirmationEmail(booking);
                
                // Redirect to a confirmation view
                return RedirectToAction("BookingConfirmation", new { orderNumber = model.OrderNumber });
            }
            else
            {
                // If the model state is invalid, return to the booking page with validation messages
                return View("Booking", model);
            }
        }

        public IActionResult BookingConfirmation(string orderNumber)
        {
            var booking = _bookingService.GetBookingByOrderNr(orderNumber);
            if (booking == null)
            {
                return NotFound();
            }

            var model = new BookingViewModel
            {
                OrderNumber = booking.OrderNumber,
                OrderDate = booking.OrderDate,
                Email = booking.Email,
                Name = booking.Name,
                FromDate = booking.DateStart,
                ToDate = booking.DateEnd,
                CabinId = booking.CabinNr,
                TotalPrice = booking.TotalPrice,
                NumberOfNights = booking.NumberOfNights,
            };
            return View(model);
        }

        public IActionResult ViewBooking(string orderNumber)
        {
            var booking = _bookingService.GetBookingByOrderNr(orderNumber);
            if (booking == null)
            {
                return NotFound();
            }
            var hasReview = false;
            var reviewText = "";
            if (_reviewService.GetReviewByOrderNr(orderNumber) != null)
            {
                hasReview = true;
                reviewText = _reviewService.GetReviewByOrderNr(orderNumber).ReviewText;
            }

            var model = new BookingViewModel
            {
                OrderNumber = booking.OrderNumber,
                isCancelled = booking.IsCancelled,
                OrderDate = booking.OrderDate,
                Email = booking.Email,
                Name = booking.Name,
                FromDate = booking.DateStart,
                ToDate = booking.DateEnd,
                CabinId = booking.CabinNr,
                TotalPrice = booking.TotalPrice,
                NumberOfNights = booking.NumberOfNights,
                HasReview = hasReview,
                ReviewText = reviewText
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CancelBooking(string orderNumber)
        {
            _bookingService.CancelBooking(orderNumber);
            return RedirectToAction("ViewBooking", new { orderNumber });
        }

        public JsonResult DelayedRedirect()
        {
            return Json(new {success = true, message = "Action triggered sucesffully!" });
        }

        [HttpPost]
        public IActionResult CreateReview(ReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var reviewDTO = new ReviewDTO
                    {
                        OrderNumber = model.OrderNumber,
                        Name = model.Name,
                        ReviewText = model.ReviewText,
                        ReviewDate = DateTime.UtcNow
                        
                    };
                    _reviewService.AddReview(reviewDTO);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return RedirectToAction("ViewBooking", new { orderNumber = model.OrderNumber });
        }

        [HttpPost]
        public IActionResult EditReview(ReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var reviewDTO = new ReviewDTO
                    {
                        OrderNumber = model.OrderNumber,
                        Name = model.Name,
                        ReviewText = model.ReviewText,
                        ReviewDate = DateTime.UtcNow
                    };
                    _reviewService.UpdateReview(reviewDTO);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return RedirectToAction("ViewBooking", new { orderNumber = model.OrderNumber });
    }
}
}