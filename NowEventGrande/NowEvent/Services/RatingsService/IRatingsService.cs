using NowEvent.Models;

namespace NowEvent.Services.RatingsService
{
    public interface IRatingsService
    {
        bool RatingStatus(int eventId);
        void SaveRatings(Rating rating);
    }
}
