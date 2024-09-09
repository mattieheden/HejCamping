using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HejCamping.Web.Models;
using Microsoft.AspNetCore.Authorization;
using HejCamping.Application.Interfaces;

namespace HejCamping.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReviewService _reviewService;
        
        public HomeController(ILogger<HomeController> logger, IReviewService reviewService)
        {
            _logger = logger;
            _reviewService = reviewService;
        }

        public JsonResult DelayedRedirect()
        {
        // Your action logic here

        // Return a JSON response
        return Json(new { success = true, message = "Action triggered successfully!" });
        }
        

        // Index action
        public IActionResult Index()
        {
            var reviews = _reviewService.GetReviews();
            return View(reviews);
        }

        // Privacy action
        public IActionResult Privacy()
        {
            return View();
        }
            
        [Authorize]
        public IActionResult AdminPortal()
        {
            return View();
        }

        // Error action
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
