using NowEvent.Data.Repositories.RatingsRepository;
using NowEvent.Models;

namespace NowEvent.Services.RatingsService
{
    public class RatingsService : IRatingsService
    {
        private readonly IRatingsRepository _ratingsRepository;

        public RatingsService(IRatingsRepository ratingsRepository)
        {
            _ratingsRepository = ratingsRepository;
        }

        public bool RatingStatus(int eventId)
        {
            Rating ratingById = _ratingsRepository.RatingStatus(eventId);
            return ratingById != null!;
        }

        public void SaveRatings(Rating rating)
        {
            _ratingsRepository.SaveRatings(rating);
        }
    }
}
