using HejCamping.Domain.Entities;

namespace HejCamping.Domain.Repositories


{
    public interface IReviewRepository
    {
        List<Review> GetReviews();
        Review GetReviewByOrderNr(string orderNumber);
        Task <IEnumerable<Review>> GetAllReviews();
        void AddReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(string orderNumber);
    }
}