using Microsoft.EntityFrameworkCore;
using VideoGameCharacterApi.Data;
using VideoGameCharacterApi.Interfaces;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _dataContext;

        public ReviewRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreateReview(Review review)
        {
            _dataContext.Reviews.Add(review);
            return Save();
        }

        public Review GetReview(int reviewId)
        {
            
            return _dataContext.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _dataContext.Reviews.OrderBy(r=>r.Id).ToList();
        }

        public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
        {
            return _dataContext.Reviews.Where(r=> r.Pokemon.Id == pokeId).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _dataContext.Reviews.Any(r=>r.Id == reviewId);
        }
        public bool Save()
        {
            var result = _dataContext.SaveChanges();
            return result > 0 ? true : false;
        }
    }
}
