using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HejCamping.Web.Models;
using HejCamping.Application.Interfaces;


namespace HejCamping.ApplicationServices;


public class AdminPortalController : Controller
{

  private readonly IBookingService _bookingService;

  public AdminPortalController(IBookingService bookingService)
  {
      _bookingService = bookingService;
  }

  [Authorize]
  public IActionResult Dashboard()
  {    

    var bookings = _bookingService.GetAllBookings();

    List<BookingViewModel> bookingViewModel = new List<BookingViewModel>();
    foreach (var booking in bookings)
    {
        bookingViewModel.Add(new BookingViewModel
        {
            OrderNumber = booking.OrderNumber,
            IsCancelled = booking.IsCancelled,
            FromDate = booking.DateStart,
            ToDate = booking.DateEnd,
            OrderDate = booking.OrderDate,
            CabinId = booking.CabinNr,
            Name = booking.Name,
            Email = booking.Email,
            PricePerNight = booking.PricePerNight,
            NumberOfNights = booking.NumberOfNights,
            TotalPrice = booking.TotalPrice
        });
      
    }
   
    ViewBag.Bookings =  bookingViewModel;

    return View();
  }

  
  public JsonResult GetAllBookings()
  {
    
    try
    {
        var bookings = _bookingService.GetAllBookings();
        
        return Json(bookings);
    }
    catch (Exception)
    {
        // Log the exception (using a logging framework)
        // Return an error view or message
        return Json( new { message = "An error occurred while loading bookings."});
    }
  }

}
