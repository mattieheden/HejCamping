using HejCamping.Domain;

namespace HejCamping.ApplicationServices

{
    public interface IReviewService
    {
        void AddReview(ReviewDTO review);
        ReviewDTO GetReviewByOrderNr(string orderNumber);
        void UpdateReview(ReviewDTO review);
        List<ReviewDTO> GetReviews();
    }
}