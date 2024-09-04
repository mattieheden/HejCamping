using HejCamping.ApplicationServices;
using HejCamping.Domain;
using Microsoft.AspNetCore.Mvc;

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
            return View(reviews);
        }

        public IActionResult CreateReview()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MakeReview(ReviewDTO reviewDTO)
        {
            var review = new Review(reviewDTO.OrderNumber, reviewDTO.Name, reviewDTO.ReviewText, reviewDTO.ReviewDate);
            _reviewRepository.AddReview(review);
            return RedirectToAction("Index");
        }

        public IActionResult EditReview()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReview(ReviewDTO reviewDTO)
        {
            var existingReview = _reviewRepository.GetReviewByOrderNr(reviewDTO.OrderNumber);
            if (existingReview != null)
            {
            existingReview.Name = reviewDTO.Name;
            existingReview.ReviewText = reviewDTO.ReviewText;
            existingReview.ReviewDate = reviewDTO.ReviewDate;

            await _reviewRepository.UpdateReview(existingReview);
            return RedirectToAction("Index");
            }
            else
            {
                return NotFound("Review not found");
            }
        }
    }
}
