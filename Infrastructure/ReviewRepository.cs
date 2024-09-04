using HejCamping.Domain;
using Microsoft.EntityFrameworkCore;

namespace HejCamping.Infrastructure
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Review> GetReviews()
        {
            // Mocked data
            var reviews = new List<Review>{};
            var review1 = new Review ( "1234", "John Doe", "Great camping experience!", DateTime.Now );
            var review2 = new Review("5678","Jane Doe","Not so great place!",DateTime.Now);
            var review3 = new Review("91011","John Doe","Great place!",DateTime.Now);
            reviews.Add(review1);
            reviews.Add(review2);
            reviews.Add(review3);
            return reviews;

           /* return _context.Reviews.ToList();*/
        }

        public Review GetReviewByOrderNr(string orderNumber)
        {
            return _context.Reviews.FirstOrDefault(r => r.OrderNumber == orderNumber);
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task AddReview(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            Console.WriteLine("Review repository added");
        }

        public async Task UpdateReview(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }
    }
}