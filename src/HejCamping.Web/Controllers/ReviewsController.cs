using HejCamping.ApplicationServices;
using HejCamping.Domain.Entities;
using HejCamping.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HejCamping.Web.Models;

namespace HejCamping.ReviewsController
{
    public class ReviewsController : Controller
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public IActionResult Index()
        {
            var reviews = _reviewRepository.GetReviews();
            List<ReviewDTO> model = new List<ReviewDTO>();
            foreach (var review in reviews)
            {
                model.Add(new ReviewDTO
                {
                    OrderNumber = review.OrderNumber,
                    Name = review.Name,
                    ReviewText = review.ReviewText,
                    ReviewDate = review.ReviewDate
                });
            }

            return View(model);
        }

        public IActionResult CreateReview()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MakeReview(ReviewViewModel model)
        {
            var review = new Review(model.OrderNumber, model.Name, model.ReviewText, DateTime.Now);
            _reviewRepository.AddReview(review);
            return RedirectToAction("Index");
        }

        public IActionResult EditReview()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReview(ReviewViewModel model)
        {
            var existingReview = _reviewRepository.GetReviewByOrderNr(model.OrderNumber);
            if (existingReview != null)
            {
            existingReview.ReviewText = model.ReviewText;

            _reviewRepository.UpdateReview(existingReview);
            return RedirectToAction("Index");
            }
            else
            {
                return NotFound("Review not found");
            }
        }
    }
}
