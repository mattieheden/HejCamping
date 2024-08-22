using HejCamping.Domain;

namespace HejCamping.Infrastructure
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public Review GetReviewByOrderNr(string orderNumber)
        {
            return _context.Reviews.FirstOrDefault(r => r.OrderNumber == orderNumber);
        }

        public void AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public void UpdateReview(Review review)
        {
            _context.Reviews.Update(review);
            _context.SaveChanges();
        }
    }
}