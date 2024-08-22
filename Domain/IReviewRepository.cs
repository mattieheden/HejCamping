namespace HejCamping.Domain

{
    public interface IReviewRepository
    {
        Review GetReviewByOrderNr(string orderNumber);
        void AddReview(Review review);
        void UpdateReview(Review review);
    }
}