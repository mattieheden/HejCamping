using HejCamping.ApplicationServices;
using Microsoft.AspNetCore.Mvc;

namespace HejCamping.Controllers
{
    // Hämta databas context?
    // private readonly IReviewService _reviewService;
public class ReviewController : Controller
{
    // GET: ReviewController
    public IActionResult Index()
    {
        return View();
    }
}



}