namespace HejCamping.Domain

{
    public interface IReviewRepository
    {
        List<Review> GetReviews();
        Review GetReviewByOrderNr(string orderNumber);
        void AddReview(Review review);
        void UpdateReview(Review review);
    }
}