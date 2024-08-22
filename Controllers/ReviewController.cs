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
        //             var reviews = _reviewService.GetReviews();
        //             return View(reviews); ??
        return View();
    }
}



}