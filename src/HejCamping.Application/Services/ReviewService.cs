using HejCamping.Domain.Entities;
using HejCamping.Domain.Repositories;
using HejCamping.Application.Interfaces;
using HejCamping.Application.DTOs;

namespace HejCamping.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }


        public List<ReviewDTO> GetReviews()
        {
            var reviews = _reviewRepository.GetReviews();
            var  reviewDTOs = new List<ReviewDTO>();
            foreach (var review in reviews)
            {
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
            if (newReview == null)
            {
                return null;
            }
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