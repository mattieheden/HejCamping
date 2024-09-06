using HejCamping.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using HejCamping.Infrastructure.Persistence;
using HejCamping.Domain.Repositories;

namespace HejCamping.Infrastructure.Repositories
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
           return _context.Reviews.ToList();
        }

        public Review GetReviewByOrderNr(string orderNumber)
        {
            return _context.Reviews.FirstOrDefault(r => r.OrderNumber == orderNumber);
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            return await _context.Reviews.ToListAsync();
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

        public void DeleteReview(string orderNumber)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.OrderNumber == orderNumber);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
            }
        }
    }
}