using NowEvent.Data.Repositories.RatingsRepository;
using NowEvent.Models;
using NowEvent.Services.VerificationService;

namespace NowEvent.Services.RatingsService
{
    public class RatingsService : IRatingsService
    {
        private readonly IRatingsRepository _ratingsRepository;
        private readonly IVerificationService _verificationService;

        public RatingsService(IRatingsRepository ratingsRepository, IVerificationService verificationService)
        {
            _ratingsRepository = ratingsRepository;
            _verificationService = verificationService;
        }

        public bool RatingStatus(int eventId)
        {
            Rating ratingById = _ratingsRepository.RatingStatus(eventId);
            return _verificationService.VerifyIfRecordExists(ratingById);
        }

        public void SaveRatings(Rating rating)
        {
            _ratingsRepository.SaveRatings(rating);
        }
    }
}
