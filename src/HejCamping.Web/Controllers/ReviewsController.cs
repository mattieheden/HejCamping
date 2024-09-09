using HejCamping.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HejCamping.Web.Models;
using HejCamping.Application.DTOs;

namespace HejCamping.Web.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public IActionResult Index()
        {
            var reviews = _reviewService.GetReviews();
            ViewBag.Reviews = reviews;
            return View();
        }

        public IActionResult CreateReview()
        {
            return View();
        }


        public IActionResult UpdateReview(ReviewViewModel model)
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
            return View("Index");
        }
    }
}