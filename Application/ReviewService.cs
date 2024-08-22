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