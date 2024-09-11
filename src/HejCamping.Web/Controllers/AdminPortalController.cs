using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HejCamping.Web.Models;
using HejCamping.Application.Interfaces;
using HejCamping.Web.Controllers;
using System.Text.Json;
using System;
using Newtonsoft.Json;
using HejCamping.Application.DTOs;
using HejCamping.Domain.Repositories;

namespace HejCamping.ApplicationServices;

public class AdminPortalController : Controller
{
  private readonly IBookingService _bookingService;

  private readonly IBookingRepository _bookingRepository;

  public AdminPortalController(IBookingService bookingService, IBookingRepository bookingRepository)
  {
    _bookingService = bookingService;
    _bookingRepository = bookingRepository;
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
