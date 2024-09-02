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
            Console.WriteLine("Getting reviews from repository");
            var reviews = _reviewRepository.GetReviews();
            var  reviewDTOs = new List<ReviewDTO>();
            foreach (var review in reviews)
            {
                Console.WriteLine("Handling review: " + review.OrderNumber);
                reviewDTOs.Add(new ReviewDTO
                {
                    OrderNumber = review.OrderNumber,
                    Name = review.Name,
                    ReviewText = review.ReviewText,
                    ReviewDate = review.ReviewDate
                });
            }
            return reviewDTOs;

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