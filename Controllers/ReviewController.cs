using HejCamping.ApplicationServices;
using Microsoft.AspNetCore.Mvc;

namespace HejCamping.Controllers
{
    // Hämta databas context?
    // private readonly IReviewService _reviewService;
public class ReviewController : Controller
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }
    public IActionResult Index()
    {
        var reviews = _reviewService.GetReviews();
        if (reviews == null)
        {
            reviews = new List<ApplicationServices.ReviewDTO>();
        }
        // Breakpoint här för att se att reviews innehåller data
        return View(reviews);
    }

    [HttpGet]
    public IActionResult GetAllReviews()
    {
        var reviews = _reviewService.GetReviews();
        if (reviews == null)
        {
            reviews = new List<ApplicationServices.ReviewDTO>();
        }
        return Json(reviews);
    }
}



}