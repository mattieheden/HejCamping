using HejCamping.ApplicationServices;
using HejCamping.Domain.Entities;
using HejCamping.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HejCamping.Web.Models;

namespace HejCamping.ReviewsController
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

        [HttpPost]
        public IActionResult MakeReview(ReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var reviewDTO = new ReviewDTO
                    {
                        OrderNumber = "HEJ123", // Mock order number..
                        Name = model.Name,
                        ReviewText = model.ReviewText,
                        ReviewDate = DateTime.UtcNow
                        
                    };
                    _reviewService.AddReview(reviewDTO);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return View("Index");
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