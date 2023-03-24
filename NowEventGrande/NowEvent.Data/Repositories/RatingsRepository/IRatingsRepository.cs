using NowEvent.Models;

namespace NowEvent.Data.Repositories.RatingsRepository
{
    public interface IRatingsRepository
    {
        bool RatingStatus(int eventId);
        void SaveRatings(Rating rating);
    }
}
