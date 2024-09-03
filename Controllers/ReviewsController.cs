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

        // GET: Reviews
        public IActionResult Index()
        {
            /*var reviews = _reviewService.GetReviews();
            foreach (var review in reviews)
            {
                Console.WriteLine(review.OrderNumber);
            }
            return View(reviews);  Denna som körs på mockade i homecontroller nu!  */
            var reviews = _reviewRepository.GetReviews();
            return View(reviews);
        }

        // Get: Reviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // Post: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("OrderNumber,Name,ReviewText,ReviewDate")] Review review)
        {
            if (ModelState.IsValid)
            {
                _reviewRepository.AddReview(review);
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }


    }
}