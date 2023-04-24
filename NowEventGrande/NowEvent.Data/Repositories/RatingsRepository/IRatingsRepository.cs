using NowEvent.Models;

namespace NowEvent.Data.Repositories.RatingsRepository
{
    public interface IRatingsRepository
    {
        Rating RatingStatus(int eventId);
        void SaveRatings(Rating rating);
    }
}
