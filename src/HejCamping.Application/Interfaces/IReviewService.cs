using HejCamping.Domain;
using HejCamping.Application.DTOs;

namespace HejCamping.Application.Interfaces

{
    public interface IReviewService
    {
        void AddReview(ReviewDTO review);
        ReviewDTO GetReviewByOrderNr(string orderNumber);
        void UpdateReview(ReviewDTO review);
        List<ReviewDTO> GetReviews();
        void DeleteReview(string orderNumber);
    }
}