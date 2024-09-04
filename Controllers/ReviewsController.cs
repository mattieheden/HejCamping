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
        public IActionResult MakeReview(string orderNumber, string name, string reviewText)
        {

            var review = new Review(orderNumber, name, reviewText, DateTime.Now);
            _reviewRepository.AddReview(review);
            Console.WriteLine("Hej ReviewController added");
            return RedirectToAction("Index");
        }

        public IActionResult EditReview()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReview(string orderNumber, string reviewText)
        {
            var existingReview = _reviewRepository.GetReviewByOrderNr(orderNumber);
            if (existingReview != null)
            {
            existingReview.ReviewText = reviewText;

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
