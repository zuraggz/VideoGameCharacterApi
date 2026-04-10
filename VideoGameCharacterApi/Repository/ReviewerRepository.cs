using Microsoft.EntityFrameworkCore;
using VideoGameCharacterApi.Data;
using VideoGameCharacterApi.Interfaces;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _dataContext;
        public ReviewerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _dataContext.Reviewers.Add(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _dataContext.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _dataContext.Reviewers.OrderBy(r => r.Id).ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _dataContext.Reviews.Where(r=>r.Reviewer.Id==reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _dataContext.Reviewers.Any(r=>r.Id==reviewerId);
        }
        public bool Save()
        {
            var result = _dataContext.SaveChanges();
            return result > 0 ? true : false;
        }
    }
}
