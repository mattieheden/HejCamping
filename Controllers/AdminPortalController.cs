using Microsoft.AspNetCore.Mvc;
using HejCamping.Models;


namespace HejCamping.Controllers;

public class AdminPortalController : Controller
{
    
    [HttpGet]
    public IActionResult DashBoard(Cabin model)
    {
        return View(model);
    
    }
} 

