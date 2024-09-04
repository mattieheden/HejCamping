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
            var viewModel = reviews.Select(r => new ReviewViewModel
            {
                CustomerName = r.CustomerName,
                ReviewText = r.ReviewText,
                ReviewDate = r.ReviewDate

                return View(viewModel);
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReviewDTO reviewDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var reviewDto = new ReviewDTO
                    {
                        CustomerName = reviewDTO.CustomerName,
                        ReviewText = reviewDTO.ReviewText,
                        ReviewDate = DateTime.Now
                    };
                    await _reviewRepository.AddReview(reviewDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (UserCreationFailedException ex)
                {
                    var causeMessage = ex.InnerException?.Message ?? "There was an issue creating the review. Please try again.";
                    ModelState.AddModelError(string.Empty, causeMessage);
                }
            }
            return View(reviewDTO);
        }

        public async Task<IActionResult> Edit(string orderNumber)
        {
            var review = await _reviewRepository.GetReviewByOrderNr(orderNumber);
            if (review == null)
            {
                return NotFound();
            }
            var viewModel = new ReviewViewModel
            {
                CustomerName = review.CustomerName,
                ReviewText = review.ReviewText,
                ReviewDate = review.ReviewDate
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string orderNumber)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var review = await _reviewRepository.GetReviewByOrderNr(orderNumber);
                    if (review == null)
                    {
                        return NotFound();
                    }
                    review.CustomerName = review.CustomerName;
                    review.ReviewText = review.ReviewText;
                    review.ReviewDate = DateTime.Now;
                    await _reviewRepository.UpdateReview(review);
                    return RedirectToAction(nameof(Index));
                }
                catch (UserUpdateFailedException ex)
                {
                    var causeMessage = ex.InnerException?.Message ?? "There was an issue updating the review. Please try again.";
                    ModelState.AddModelError(string.Empty, causeMessage);
                }
            }
            return View(review);
        }
    }
}