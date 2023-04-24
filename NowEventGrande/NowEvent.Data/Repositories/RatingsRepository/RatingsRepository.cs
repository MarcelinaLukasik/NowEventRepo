using NowEvent.Models;

namespace NowEvent.Data.Repositories.RatingsRepository
{
    public class RatingsRepository : IRatingsRepository
    {
        private readonly AppDbContext _appDbContext;

        public RatingsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Rating RatingStatus(int eventId)
        {
            return _appDbContext.Rating.FirstOrDefault(g => g.EventId == eventId)!;
        }

        public void SaveRatings(Rating rating)
        {
            _appDbContext.Rating.Add(rating);
            _appDbContext.SaveChanges();
        }
    }
}
