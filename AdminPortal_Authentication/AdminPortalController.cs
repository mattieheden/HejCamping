using Microsoft.AspNetCore.Mvc;
using HejCamping.Models;
//using Newtonsoft.Json;  // Include this if using Newtonsoft.Json
using System.Text.Json;
using System.Text.Json.Serialization;
using System;

using Microsoft.AspNetCore.Authorization;

namespace HejCamping.ApplicationServices;

using HejCamping.Controllers;

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
      
    ViewBag.Bookings =  bookings;

    return View();
  }

  public JsonResult GetAllBookings()
  {
    //var bookings = _bookingService.GetAllBookings();
    //return Json(bookings);

    try
    {
        var bookings = _bookingService.GetAllBookings();
        //Console.WriteLine(bookings.Name);
        //ViewBag.Bookings = bookings;

        //string jsonString = JsonSerializer.Serialize(bookings);
        //string jsonString = JsonConvert.SerializeObject(bookings);
        //Console.WriteLine(jsonString);
        
        //return Json(jsonString);
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

// Paste the follwing into BookingService.cs
/* 
  public List<BookingDTO> GetAllBookings()
        {
            var bookings = _bookingRepository.GetAllBookings();

            return bookings.Select(booking => new BookingDTO{
                OrderNumber = booking.OrderNumber,
                IsCancelled = booking.IsCancelled,
                OrderDate = booking.OrderDate,
                Email = booking.Email,
                Name = booking.Name,
                DateStart = booking.DateStart,
                DateEnd = booking.DateEnd,
                CabinNr = booking.CabinNr,
                TotalPrice = booking.TotalPrice
            }).ToList();
  }

  // This one into IBookingService.cs
  List<BookingDTO> GetAllBookings();

  // This one into BookingRepository.cs
  public List<Booking> GetAllBookings()
        {
            // Return some data (e.g., from a database or in-memory collection)
            return new List<Booking>(); // dummy implementation for now
  }
*/
