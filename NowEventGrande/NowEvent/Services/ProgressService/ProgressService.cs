using NowEvent.Data;
using NowEvent.Models;
using NowEvent.Models.Constants;
using NowEvent.Services.VerificationService;

namespace NowEvent.Services.ProgressService
{
    public class ProgressService : IProgressService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IVerificationService _verificationService;
        private readonly IOfferRepository _offerRepository;
        private int _progressLevel;
        private const int FullProgress = 3;
        private bool _posted;
        public ProgressService(IEventRepository eventRepository, IGuestRepository guestRepository, 
            IVerificationService verificationService, IOfferRepository offerRepository)
        {
            _eventRepository = eventRepository;
            _guestRepository = guestRepository;
            _verificationService = verificationService;
            _offerRepository = offerRepository;
            
        }
        public async Task<bool> CheckEventStatus(int id)
        {
            _progressLevel = await GetChecklistCount(id);
            Offer offer = _offerRepository.GetOfferByEventId(id);
            if (offer != null)
            {
                _posted = true;
            }
            if (_progressLevel != FullProgress)
            {
                 await _eventRepository.SetStatus(id, EventStatuses.Incomplete);
                 return false;
            }
            if(_posted)
            {
                await _eventRepository.SetStatus(id, EventStatuses.Posted);
                return true;
            }
            else
            {
                await _eventRepository.SetStatus(id, EventStatuses.Completed);
                return true;
            }
        }

        public async Task<int> GetChecklistCount(int id)
        {
            var checklistCount = 0;
            bool isLargeScale = _eventRepository.CheckIfLargeSize(id);

            var guests = _guestRepository.AllGuestsByEventId(id);
            if (guests.Any() || isLargeScale)
            {
                checklistCount++;
            }

            var budgetStatus = _verificationService.CheckBudgetFullStatus(id);
            if (budgetStatus)
            {
                checklistCount++;
            }

            var checkDate = await _eventRepository.CheckDateAndTimeByEventId(id);
            if (checkDate)
            {
                checklistCount++;
            }

            return checklistCount;
        }
    }
}
