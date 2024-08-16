using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HejCamping.Models;

namespace HejCamping.Controllers;

// DateController.cs
public class DatePickerController : Controller
{
    private readonly ILogger<DatePickerController> _logger;

    public DatePickerController(ILogger<DatePickerController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult DatePicker()
    {
        return View();
    }

    [HttpPost]
    public IActionResult DatePicker(DatePickerModel model)
    {
        // Do something with the selected date
        return View(model);
    }

    [HttpPost]
    public IActionResult FromDate(DatePickerModel model)
    {
        // Do something with the selected date
        return View(model);
    }

    [HttpPost]
    public IActionResult ToDate(DatePickerModel model)
    {
        // Do something with the selected date
        return View(model);
    }
}