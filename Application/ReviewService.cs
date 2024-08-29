using HejCamping.Domain;
using HejCamping.Migrations;

namespace HejCamping.ApplicationServices
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

    /*    public List<ReviewDTO> GetReviews()
        {
            var reviews = _reviewRepository.GetReviews();
            return reviews.Select(r => new ReviewDTO
            {
                OrderNumber = r.OrderNumber,
                Name = r.Name,
                ReviewText = r.ReviewText,
                ReviewDate = r.ReviewDate
            }).ToList();
        }*/
        public List<ReviewDTO> GetReviews()
        {
            // Create a list of ReviewDTO objects
            var reviews = new List<ReviewDTO>
            {
                new ReviewDTO { OrderNumber = "1234", Name = "John Doe", ReviewText = "Great camping experience!", ReviewDate = DateTime.Now },
                new ReviewDTO { OrderNumber = "5678", Name = "Jane Smith", ReviewText = "Beautiful scenery!", ReviewDate = DateTime.Now.AddDays(-1) },
                new ReviewDTO { OrderNumber = "9012", Name = "Bob Johnson", ReviewText = "Friendly staff!", ReviewDate = DateTime.Now.AddDays(-2) }
            };

            return reviews;
        }

        public void AddReview(ReviewDTO review)
        {
            _reviewRepository.AddReview(new Review(review.OrderNumber, review.Name, review.ReviewText, review.ReviewDate));
        }

        public ReviewDTO GetReviewByOrderNr(string orderNumber)
        {
            var newReview = _reviewRepository.GetReviewByOrderNr(orderNumber);
            return new ReviewDTO
            {
                OrderNumber = newReview.OrderNumber,
                Name = newReview.Name,
                ReviewText = newReview.ReviewText,
                ReviewDate = newReview.ReviewDate
            };
        }

        public void UpdateReview(ReviewDTO review)
        {
            _reviewRepository.UpdateReview(new Review (review.OrderNumber, review.Name, review.ReviewText, review.ReviewDate));
        }
    }
}