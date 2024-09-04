namespace HejCamping.Domain

{
    public interface IReviewRepository
    {
        List<Review> GetReviews();
        Review GetReviewByOrderNr(string orderNumber);
        Task <IEnumerable<Review>> GetAllReviews();
        Task AddReview(Review review);
        Task UpdateReview(Review review);
    }
}