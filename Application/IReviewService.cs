using HejCamping.Domain;

namespace HejCamping.ApplicationServices

{
    public interface IReviewService
    {
        ReviewDTO GetReviewByOrderNr(string orderNumber);
        void AddReview(ReviewDTO review);
    }
}